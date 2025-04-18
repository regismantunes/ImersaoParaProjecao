using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using ImmersionToProjection.Extensions;
using ImmersionToProjection.Model;
using ImmersionToProjection.Service.Extraction.Patterns;

namespace ImmersionToProjection.Service.Extraction;

public class ImmersionExtractor(IRegexHelper regexHelper, IFormatProvider formatProvider, string messageTitleFormat) : IImmersionExtractor
{
    public string GetTextToProjection(IEnumerable<ImmersionDay> immersionDays)
    {
        var sbFinal = new StringBuilder();

        foreach (var immersionDay in immersionDays)
        {
            foreach (var immersionItem in immersionDay.Items)
            {
                sbFinal
                    .AppendLine(immersionDay.Day.GetLocalizedName(formatProvider))
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

    private const string errorMessage = @"Não foi possível identificar o texto para extração da imersão.";

    private async Task<string> GetMessageTitle(string text)
    {
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
            var singleMessageTitle = text[n..(n + matchBibleReading.Index)]
                .TrimSpecialCharacters()
                .Replace("\r", string.Empty)
                .Replace("\n", string.Empty);

            var o = n + matchBibleReading.Index + matchBibleReading.Length + 1;
            var p = o + text[o..].IndexOf('\n');
            var bibleReading = text[o..p].TrimSpecialCharacters();
            messageTitle = string.Format(messageTitleFormat, Convert.ToInt16(matchMessageNumber.Value), singleMessageTitle, bibleReading);
        }

        return await Task.FromResult(messageTitle);
    }

    private async Task<IEnumerable<ImmersionDay>> GetImmersionDays(string text)
    {
        var immersionDays = new List<ImmersionDay>();
        var daysOfWeek = Enum.GetValues<DayOfWeek>()
            .Cast<DayOfWeek>()
            .ToArray();

        for (var d = 0; d < daysOfWeek.Length; d++)
        {
            var dayOfWeek = daysOfWeek[d];
            var dayOfWeekName = dayOfWeek.GetLocalizedName(formatProvider);
            var matchStart = Regex.Match(text, $@"\s+{dayOfWeekName}\s+", RegexOptions.IgnoreCase);
            if (!matchStart.Success)
            {
                if (d == 0)
                    throw new InvalidDataException(errorMessage);

                break;
            }

            var i = matchStart.Index + matchStart.Length;

            var matchPoints = regexHelper.GetEndOfDaillyPoint()
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
                    if (regexHelper.GetImmersionPoint().IsMatch(b))
                        return string.Concat(a, "\n", b);
                    if (a.EndsWith('-'))
                        return string.Concat(a, b);
                    return string.Concat(a, " ", b);
                });

            var pointMatches = dailyPointsText.Split('\n');

            immersionDays.Add(new ImmersionDay() { Day = dayOfWeek, Items = pointMatches });
        }

        return await Task.FromResult(immersionDays.OrderBy(d => d.Day == DayOfWeek.Sunday ? 7 : (int)d.Day));
    }

    private ImmersionWeek ConvertPdfTextToImmersionWeek(string text)
    {
        var messageTitleTask = GetMessageTitle(text);
        var immersionDaysTask = GetImmersionDays(text);

        Task.WaitAll(messageTitleTask, immersionDaysTask);

        return new ImmersionWeek 
        { 
            MessageTitle = messageTitleTask.Result, 
            ImmersionDays = immersionDaysTask.Result 
        };
    }
}