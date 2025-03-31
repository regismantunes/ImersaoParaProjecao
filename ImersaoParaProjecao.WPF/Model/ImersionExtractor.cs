using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using ImersaoParaProjecao.Model.Interfaces;
using ImersaoParaProjecao.Helper.Interfaces;

namespace ImersaoParaProjecao.Model;

public class ImersionExtractor(IRegexHelper regexHelper) : IImersionExtractor
{
    public string GetImersionToProjection(Dictionary<string, string[]> imersionDays)
    {
        var sbFinal = new StringBuilder();

        foreach (var item in imersionDays)
        {
            foreach (var imersionItem in item.Value)
            {
                sbFinal
                    .AppendLine(item.Key)
                    .AppendLine(imersionItem)
                    .AppendLine();
            }
        }

        return sbFinal.ToString().Trim();
    }

    public Dictionary<string, string[]>? GetImersionDays(string filePath, out string? messageTitle)
    {
        var fileContent = GetPdfText(filePath);
        if (string.IsNullOrEmpty(fileContent))
        {
            messageTitle = null;
            return null;
        }

        return ConvertPdfTextToProjectionText(fileContent, out messageTitle);
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

    private Dictionary<string, string[]> ConvertPdfTextToProjectionText(string text, out string messageTitle)
    {
        const string errorMessage = @"Não foi possível identificar o texto para extração da imersão.";

        var matchTitle = regexHelper.GetMessageTitle().Match(text);
        if (!matchTitle.Success)
            throw new InvalidDataException(errorMessage);

        var n = matchTitle.Index + matchTitle.Length;
        var o = n + text[n..].IndexOf('\n');
        messageTitle = text[matchTitle.Index..o].Trim();

        var imersionDays = new Dictionary<string, string[]>();
        var daysOfWeek = new[] { "SEGUNDA-FEIRA", "TERÇA-FEIRA", "QUARTA-FEIRA", "QUINTA-FEIRA", "SEXTA-FEIRA", "SÁBADO", "DOMINGO" };

        for (var d = 0; d < daysOfWeek.Length; d++)
        {
            var dayOfWeek = daysOfWeek[d];
            var matchStart = Regex.Match(text, $@"\s+{dayOfWeek}\s+");
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

            imersionDays.Add(dayOfWeek, pointMatches);
        }

        return imersionDays.OrderBy(x => Array.IndexOf(daysOfWeek, x))
                           .ToDictionary();
    }
}