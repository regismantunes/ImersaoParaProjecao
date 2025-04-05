using ImmersionToProjection.Helper.Interfaces;
using System.Text.RegularExpressions;

namespace ImmersionToProjection.Helper;

public class RegexHelper(RegexHelperPatterns patterns) : IRegexHelper
{
    public Regex GetImmersionPoint() => new(patterns.ImmersionPoint);

    public Regex GetEndOfDaillyPoint() => new(patterns.EndOfDaillyPoint);

    public Regex GetMessageHeader() => new(patterns.MessageHeader);

    public Regex GetNumber() => new(patterns.Number);

    public Regex GetBibleReading() => new(patterns.BibleReading);
}
