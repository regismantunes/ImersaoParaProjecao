using ImmersionToProjection.Extensions;
using ImmersionToProjection.Model;
using ImmersionToProjection.Service.Formatter;
using System.Configuration;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ImmersionToProjection.Service.Extraction.Patterns;

public class PatternsHelper(IPatternsFactory patternsFactory, ICaseStringFormat titleFormatter) : IPatternsHelper
{
    private List<PatternsItem> _patterns = patternsFactory
        .GetFromConfiguration()
        .ToList();

    private IFormatProvider? _formatProvider = null;

    public int Index { get; private set; } = 0;

    public bool MoveNext()
    {
        Index++;
        _formatProvider = null;

        if (Index >= _patterns.Count)
        {
            Index = 0;
            return false;
        }
        return true;
    }

    public string GetMessageTitle(string messageTitle)
    {
        var messageTitleFormat = _patterns[Index].MessageTitleFormat;
        var titleFormat = Regex.Replace(messageTitleFormat, @"(?<title>\{{\s*T).*?\}}", match =>
        {
            var i = match.Groups["title"].Value.Length;
            return string.Concat("{0", match.Value.Substring(i));
        });
        
        return string.Format(titleFormatter, titleFormat, messageTitle);
    }

    public string GetMessageTitle(int messageNumber, string messageTitle, string bibleReading)
    {
        var messageTitleFormat = _patterns[Index].MessageTitleFormat
            .ReplaceFormatVariable("N", 0)
            .ReplaceFormatVariable("T", 1)
            .ReplaceFormatVariable("B", 2);

        return string.Format(titleFormatter, messageTitleFormat, messageNumber, messageTitle, bibleReading);
    }

    public string GetLocalizedName(DayOfWeek dayOfWeek)
    {
        if (_formatProvider == null)
            _formatProvider = CultureInfo.CreateSpecificCulture(_patterns[Index].Language);

        return dayOfWeek.GetLocalizedName(_formatProvider);
    }

    public string GetLanguage() => _patterns[Index].Language;

    public Regex GetImmersionPoint() => new(_patterns[Index].ImmersionPoint);

    public Regex GetEndOfDaillyPoint() => new(_patterns[Index].EndOfDaillyPoint);

    public Regex GetMessageHeader() => new(_patterns[Index].MessageHeader);

    public Regex GetNumber() => new(_patterns[Index].Number);

    public Regex GetBibleReading() => new(_patterns[Index].BibleReading);
}