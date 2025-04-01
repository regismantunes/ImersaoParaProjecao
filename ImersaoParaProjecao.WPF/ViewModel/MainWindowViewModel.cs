using ImersaoParaProjecao.Service.Interfaces;
using ImersaoParaProjecao.View;
using System.Windows;

namespace ImersaoParaProjecao.ViewModel;

public class MainWindowViewModel(IImmersionExtractor imersionExtractor) : BaseViewModel
{
    public ImmersionWeekView? ImmersionWeekView { get; private set; }
    public Visibility VisibilityDropFileTextBlock { get; private set; }
    public Visibility VisibilityImmersionWeekView { get; private set; }
    
    public void OnFileDropped(string filePath)
    {
        var immersionDays = imersionExtractor.ExtractFromFile(filePath, out var messageTitle);
        if (immersionDays == null)
            return;

        ImmersionWeekView = new ImmersionWeekView()
        {
            DataContext = new ImmersionWeekViewModel(immersionDays)
            {
                MessageTitle = messageTitle
            }
        };
        OnPropertyChanged(nameof(ImmersionWeekView));
    }
}