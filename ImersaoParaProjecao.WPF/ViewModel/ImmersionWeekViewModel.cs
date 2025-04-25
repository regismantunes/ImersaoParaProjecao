using ImmersionToProjection.Extensions;
using ImmersionToProjection.Model;
using ImmersionToProjection.Service.Extraction;
using ImmersionToProjection.Service.Language;
using ImmersionToProjection.Utility;
using System.Windows;

namespace ImmersionToProjection.ViewModel;

public class ImmersionWeekViewModel(ILanguageKeys languageKeys, IImmersionExtractor immersionExtractor, IFormatProvider formatProvider)
    : BaseViewModel(languageKeys)
{
    private ImmersionWeek? ImmersionWeek { get; set; }
    public string? ImmersionWeekText { get; private set; }
    public string? MessageTitle => ImmersionWeek?.MessageTitle;
    
    public void LoadImmersionWeek(string filePath)
    {
        var immersionWeek = immersionExtractor.ExtractFromFile(filePath);
        if (immersionWeek == null)
            return;

        ImmersionWeek = immersionWeek;
        var lineSpaces = Environment.NewLine + Environment.NewLine;
        var immersionDaysText = immersionWeek.ImmersionDays
            .Select(d => 
                string.Concat(
                    d.Day.GetLocalizedName(formatProvider),
                    lineSpaces,
                    string.Join(Environment.NewLine, d.Items)
                    )
                );
        ImmersionWeekText = string.Join(lineSpaces, immersionDaysText);

        OnPropertyChanged(nameof(MessageTitle));
        OnPropertyChanged(nameof(ImmersionWeekText));
    }

    public CommandHandler CopyToClipboardCommand => new(() =>
    {
        if (ImmersionWeek is null)
            return;
        
        var textToProjection = immersionExtractor.GetTextToProjection(ImmersionWeek.ImmersionDays);
        Clipboard.SetText(textToProjection);
    });
}
