using System.Text.RegularExpressions;

namespace ImersaoParaProjecao.Helper.Interfaces;

public interface IRegexHelper
{
    Regex GetEndOfDaillyPoints();
    Regex GetImersionPoints();
    Regex GetMessageHeader();
    Regex GetNumber();
    Regex GetBibleReading();
}