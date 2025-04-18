using System.Text.RegularExpressions;

namespace ImmersionToProjection.Service.Extraction.Patterns;

public interface IRegexHelper
{
    Regex GetEndOfDaillyPoint();
    Regex GetImmersionPoint();
    Regex GetMessageHeader();
    Regex GetNumber();
    Regex GetBibleReading();
}