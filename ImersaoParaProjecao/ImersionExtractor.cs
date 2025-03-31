using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using System.Text;
using System.Text.RegularExpressions;
using ImersaoParaProjecao.Utility;

namespace ImersaoParaProjecao
{
    public static partial class ImersionExtractor
    {
        public static string GetImersionToProjection(Dictionary<string, string[]> imersionDays)
        {
            var sbFinal = new StringBuilder();

            foreach(var item in imersionDays)
            {
                foreach (var imersionItem in item.Value)
                {
                    sbFinal.AppendLine(item.Key);
                    sbFinal.AppendLine(imersionItem);
                    sbFinal.AppendLine();
                }
            }

            return sbFinal.ToString().Trim();
        }

        public static Dictionary<string, string[]>? GetImersionDays(string filePath, out string? messageTitle)
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
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Erro ao extrair texto do arquivo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }

        private static Dictionary<string, string[]> ConvertPdfTextToProjectionText(string text, out string messageTitle)
        {
            const string errorMessage = @"Não foi possível identificar o texto para extração da imersão.";

            var matchTitle = RegexHelper.GetMessageHeader().Match(text);
            if (!matchTitle.Success)
                throw new InvalidDataException(errorMessage);

            var n = matchTitle.Index + matchTitle.Length + 1;
            var matchMessageNumber = RegexHelper.GetNumber().Match(matchTitle.Value);
            var matchBibleReading = RegexHelper.GetBibleReading().Match(text[n..]);
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

                var matchPoints = RegexHelper.GetEndOfDaillyPoints()
                    .Match(text[i..]);
                if (!matchPoints.Success)
                    throw new InvalidDataException(errorMessage);

                var dailyPointsText = text
                    .Substring(i, matchPoints.Index - 1)
                    .Replace("\r", string.Empty)
                    .Split('\n')
                    .Select(x => x.Trim())
                    .Where(x => x.Length > 0)
                    .Aggregate((a, b) => {
                        if (RegexHelper.GetImersionPoints().IsMatch(b))
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
}