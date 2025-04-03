using ImersaoParaProjecao.Service.Interfaces;
using ImersaoParaProjecao.View;
using System.Windows;
using System.Windows.Input;

namespace ImersaoParaProjecao.ViewModel;

public class MainWindowViewModel(IImmersionExtractor immersionExtractor, IFormatProvider formatProvider) : BaseViewModel
{
    public string DropFileMessage { get; private set; } = "Drop immersion PDF file here";
    public ImmersionWeekView? ImmersionWeekView { get; private set; }
    public Visibility VisibilityDropFileTextBlock { get; private set; }
    public Visibility VisibilityImmersionWeekView { get; private set; }
    public Cursor CurrentCursor { get; private set; } = Cursors.Arrow;

    public void OnFileDropped(string filePath)
    {
        try
        {
            CurrentCursor = Cursors.Wait;
            OnPropertyChanged(nameof(CurrentCursor));

            var immersionWeek = immersionExtractor.ExtractFromFile(filePath);
            if (immersionWeek == null)
                return;

            ImmersionWeekView = new ImmersionWeekView()
            {
                DataContext = new ImmersionWeekViewModel(immersionWeek, immersionExtractor, formatProvider)
            };
            VisibilityDropFileTextBlock = Visibility.Collapsed;
            VisibilityImmersionWeekView = Visibility.Visible;

            OnPropertyChanged(nameof(ImmersionWeekView));
            OnPropertyChanged(nameof(VisibilityDropFileTextBlock));
            OnPropertyChanged(nameof(VisibilityImmersionWeekView));
        }
        finally
        {
            CurrentCursor = Cursors.Arrow;
            OnPropertyChanged(nameof(CurrentCursor));
        }
    }
}