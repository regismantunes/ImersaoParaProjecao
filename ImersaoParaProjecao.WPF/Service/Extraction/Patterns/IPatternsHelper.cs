using System.Text.RegularExpressions;

namespace ImmersionToProjection.Service.Extraction.Patterns;

public interface IPatternsHelper
{
    int Index { get; }
    bool MoveNext();
    string GetMessageTitle(int messageNumber, string messageTitle, string bibleReading);
    string GetMessageTitle(string messageTitle);
    string GetLocalizedName(DayOfWeek dayOfWeek);
    string GetLanguage();
    Regex GetEndOfDaillyPoint();
    Regex GetImmersionPoint();
    Regex GetMessageHeader();
    Regex GetNumber();
    Regex GetBibleReading();
}