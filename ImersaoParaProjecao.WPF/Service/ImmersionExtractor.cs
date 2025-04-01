using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using ImersaoParaProjecao.Helper.Interfaces;
using ImersaoParaProjecao.Extensions;
using ImersaoParaProjecao.Service.Interfaces;
using ImersaoParaProjecao.Model;
using System.Globalization;

namespace ImersaoParaProjecao.Service;

public class ImmersionExtractor(IRegexHelper regexHelper, IFormatProvider formatProvider) : IImmersionExtractor
{
    public string GetTextToProjection(IEnumerable<ImmersionDay> immersionDays)
    {
        var sbFinal = new StringBuilder();

        foreach (var immersionDay in immersionDays)
        {
            foreach (var immersionItem in immersionDay.Items)
            {
                sbFinal
                    .AppendLine(immersionDay.Day.ToString())
                    .AppendLine(immersionItem)
                    .AppendLine();
            }
        }

        return sbFinal.ToString().Trim();
    }

    public ImmersionWeek? ExtractFromFile(string filePath)
    {
        var fileContent = GetPdfText(filePath);
        if (string.IsNullOrEmpty(fileContent))
            return null;
        
        return ConvertPdfTextToImmersionWeek(fileContent);
    }

    private static string GetPdfText(string filePath)
    {
        var sbContent = new StringBuilder();
        var pdfReader = new PdfReader(filePath);
        var pdfDocument = new PdfDocument(pdfReader);
        for (int page = 1; page <= pdfDocument.GetNumberOfPages(); page++)
        {
            var strategy = new SimpleTextExtractionStrategy();
            sbContent.Append(PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(page), strategy));
        }
        pdfDocument.Close();
        pdfReader.Close();
        return sbContent.ToString();
    }

    private ImmersionWeek ConvertPdfTextToImmersionWeek(string text)
    {
        const string errorMessage = @"Não foi possível identificar o texto para extração da imersão.";

        var matchTitle = regexHelper.GetMessageHeader().Match(text);
        if (!matchTitle.Success)
            throw new InvalidDataException(errorMessage);

        var n = matchTitle.Index + matchTitle.Length + 1;
        var matchMessageNumber = regexHelper.GetNumber().Match(matchTitle.Value);
        var matchBibleReading = regexHelper.GetBibleReading().Match(text[n..]);
        string messageTitle;
        if (!matchBibleReading.Success ||
            !matchMessageNumber.Success)
            messageTitle = matchTitle.Value;
        else
        {
            var singleMessageTitle = text[n..(n + matchBibleReading.Index)].TrimSpecialCharacters();
            var o = n + matchBibleReading.Index + matchBibleReading.Length + 1;
            var p = o + text[o..].IndexOf('\n');
            var bibleReading = text[o..p].TrimSpecialCharacters();
            messageTitle = $"Mens. {Convert.ToInt16(matchMessageNumber.Value)}: {singleMessageTitle} ({bibleReading})";
        }

        var immersionDays = new List<ImmersionDay>();
        var daysOfWeek = Enum.GetValues<DayOfWeek>()
            .Cast<DayOfWeek>()
            .ToArray();
        //var formatProvider = CultureInfo.CreateSpecificCulture("pt-BR");

        for (var d = 0; d < daysOfWeek.Length; d++)
        {
            var dayOfWeek = daysOfWeek[d];
            var dayOfWeekName = string.Format(formatProvider, "{0:ddddd}", dayOfWeek).ToUpperInvariant();
            var matchStart = Regex.Match(text, $@"\s+{dayOfWeekName}\s+");
            if (!matchStart.Success)
            {
                if (d == 0)
                    throw new InvalidDataException(errorMessage);

                break;
            }

            var i = matchStart.Index + matchStart.Length;

            var matchPoints = regexHelper.GetEndOfDaillyPoints()
                .Match(text[i..]);
            if (!matchPoints.Success)
                throw new InvalidDataException(errorMessage);

            var dailyPointsText = text
                .Substring(i, matchPoints.Index - 1)
                .Replace("\r", string.Empty)
                .Split('\n')
                .Select(x => x.Trim())
                .Where(x => x.Length > 0)
                .Aggregate((a, b) =>
                {
                    if (regexHelper.GetImersionPoints().IsMatch(b))
                        return string.Concat(a, "\n", b);
                    if (a.EndsWith('-'))
                        return string.Concat(a, b);
                    return string.Concat(a, " ", b);
                });

            var pointMatches = dailyPointsText.Split('\n');

            immersionDays.Add(new ImmersionDay() { Day = dayOfWeek , Items = pointMatches});
        }

        return immersionDays.OrderBy(x => Array.IndexOf(daysOfWeek, x))
                           .ToDictionary();
    }
}