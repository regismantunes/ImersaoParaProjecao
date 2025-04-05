using ImmersionToProjection.Model;
using ImmersionToProjection.Service.Interfaces;
using ImmersionToProjection.Utility;
using System.Windows;

namespace ImmersionToProjection.ViewModel;

public class ImmersionWeekViewModel(IImmersionExtractor immersionExtractor, IFormatProvider formatProvider) : BaseViewModel
{
    private ImmersionWeek? ImmersionWeek { get; set; }
    public string? MessageTitle => ImmersionWeek?.MessageTitle;
    public IEnumerable<ImmersionDayViewModel>? ImmersionDays => ImmersionWeek?.ImmersionDays
        .Select(d => new ImmersionDayViewModel(d, formatProvider));

    public void SetImmersionWeek(ImmersionWeek immersionWeek)
    {
        ImmersionWeek = immersionWeek;
        OnPropertyChanged(nameof(MessageTitle));
        OnPropertyChanged(nameof(ImmersionDays));
    }

    public CommandHandler CopyToClipboardCommand => new(() =>
    {
        if (ImmersionWeek is null)
            return;
        
        var textToProjection = immersionExtractor.GetTextToProjection(ImmersionWeek.ImmersionDays);
        Clipboard.SetText(textToProjection);
    });
}
