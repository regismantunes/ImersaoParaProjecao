using System.Text.RegularExpressions;

namespace ImmersionToProjection.Helper.Interfaces;

public interface IRegexHelper
{
    Regex GetEndOfDaillyPoint();
    Regex GetImmersionPoint();
    Regex GetMessageHeader();
    Regex GetNumber();
    Regex GetBibleReading();
}