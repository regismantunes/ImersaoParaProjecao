using System.Text.RegularExpressions;

namespace ImersaoParaProjecao.Helper.Interfaces;

public interface IRegexHelper
{
    Regex GetEndOfDaillyPoint();
    Regex GetImmersionPoint();
    Regex GetMessageHeader();
    Regex GetNumber();
    Regex GetBibleReading();
}