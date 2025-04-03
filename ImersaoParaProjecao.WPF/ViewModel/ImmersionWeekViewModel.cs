using ImersaoParaProjecao.Model;
using ImersaoParaProjecao.Service.Interfaces;
using ImersaoParaProjecao.Utility;
using System.Windows;

namespace ImersaoParaProjecao.ViewModel;

public class ImmersionWeekViewModel(ImmersionWeek immersionWeek, IImmersionExtractor immersionExtractor, IFormatProvider formatProvider)
{
    public string MessageTitle => immersionWeek.MessageTitle;

    public IEnumerable<ImmersionDayViewModel> ImmersionDays => immersionWeek.ImmersionDays.Select(d => new ImmersionDayViewModel(d, formatProvider));

    public CommandHandler CopyToClipboardCommand => new(() =>
    {
        var textToProjection = immersionExtractor.GetTextToProjection(immersionWeek.ImmersionDays);
        Clipboard.SetText(textToProjection);
    });
}
