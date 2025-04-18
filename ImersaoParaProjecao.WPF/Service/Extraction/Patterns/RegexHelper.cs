using System.Text.RegularExpressions;

namespace ImmersionToProjection.Service.Extraction.Patterns;

public class RegexHelper(RegexPatterns patterns) : IRegexHelper
{
    public Regex GetImmersionPoint() => new(patterns.ImmersionPoint);

    public Regex GetEndOfDaillyPoint() => new(patterns.EndOfDaillyPoint);

    public Regex GetMessageHeader() => new(patterns.MessageHeader);

    public Regex GetNumber() => new(patterns.Number);

    public Regex GetBibleReading() => new(patterns.BibleReading);
}