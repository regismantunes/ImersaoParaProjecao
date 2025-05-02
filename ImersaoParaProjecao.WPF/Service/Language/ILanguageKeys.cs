
using System.ComponentModel;

namespace ImmersionToProjection.Service.Language
{
    public interface ILanguageKeys : INotifyPropertyChanged
    {
        string ApplicationTitle { get; }
        IDictionary<string, string> AvailableLanguages { get; }
        string DefaultLanguage { get; }
        string ButtonCopyToProjection { get; }
        string ComboTheme { get; }
        string Configuration_Language { get; }
        string Configuration_MessageTitleFormat { get; }
        string Configuration_Regex_BibleReading { get; }
        string Configuration_Regex_EndOfDaillyPoint { get; }
        string Configuration_Regex_ImmersionPoint { get; }
        string Configuration_Regex_MessageHeader { get; }
        string Configuration_Regex_Number { get; }
        string Configuration_Theme { get; }
        string Error { get; }
        string ErrorImpossibleIdentifyImmersionText { get; }
        string LabelBibleReading { get; }
        string LabelConfiguration { get; }
        string LabelEndOfDaillyPoint { get; }
        string LabelExtractionParameters { get; }
        string LabelImmersionPoint { get; }
        string LabelLanguage { get; }
        string LabelMessageHeader { get; }
        string LabelMessageTtitleFormat { get; }
        string LabelNumber { get; }
        string LabelTheme { get; }
        string MessageDropImmersionPdfFileHere { get; }
        string MessageExtractingImmersionPoints { get; }
    }
}