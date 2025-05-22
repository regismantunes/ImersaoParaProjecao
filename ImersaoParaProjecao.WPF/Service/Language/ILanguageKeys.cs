
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
        string Error { get; }
        string ErrorImpossibleIdentifyImmersionText { get; }
        string LabelBibleReading { get; }
        string LabelConfiguration { get; }
        string LabelEndOfDaillyPoint { get; }
        string LabelExtractionParameters { get; }
        string LabelImmersionPoint { get; }
        string LabelLanguage { get; }
        string LabelMessageHeader { get; }
        string LabelMessageTitleFormat { get; }
        string LabelMessageTitleFormatTip { get; }
        string LabelNumber { get; }
        string LabelTheme { get; }
        string MessageDropImmersionPdfFileHere { get; }
        string MessageExtractingImmersionPoints { get; }
    }
}