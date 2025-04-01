using ImersaoParaProjecao.Helper.Interfaces;
using System.Text.RegularExpressions;

namespace ImersaoParaProjecao.Helper;

public partial class RegexHelper(RegexHelperPatterns patterns) : IRegexHelper
{
    public Regex GetImersionPoints() => new(patterns.ImersionPoints);

    public Regex GetEndOfDaillyPoints() => new(patterns.EndOfDaillyPoints);

    public Regex GetMessageHeader() => new(patterns.MessageHeader);

    public Regex GetNumber() => new(patterns.Number);

    public Regex GetBibleReading() => new(patterns.BibleReading);
}
