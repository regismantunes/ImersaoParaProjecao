using ImmersionToProjection.Extensions;
using ImmersionToProjection.Model;
using ImmersionToProjection.Service.Interfaces;
using ImmersionToProjection.Utility;
using System.Windows;
using System.Windows.Input;

namespace ImmersionToProjection.ViewModel;

public class ImmersionWeekViewModel(IImmersionExtractor immersionExtractor, IFormatProvider formatProvider) : BaseViewModel
{
    public string DropFileMessage { get; private set; } = "Drop immersion PDF file here";
    //public Visibility VisibilityDropFileTextBlock { get; private set; } = Visibility.Visible;
    public Visibility VisibilityImmersionWeekGrid { get; private set; } = Visibility.Collapsed;
    public Cursor CurrentCursor { get; private set; } = Cursors.Arrow;

    private ImmersionWeek? ImmersionWeek { get; set; }
    public string? ImmersionWeekText { get; private set; }
    public string? MessageTitle => ImmersionWeek?.MessageTitle;
    
    public async Task OnFileDroppedAsync(string filePath)
    {
        CurrentCursor = Cursors.Wait;
        DropFileMessage = "Extracting immersion points...";
        OnPropertyChanged(nameof(CurrentCursor));
        OnPropertyChanged(nameof(DropFileMessage));
        await Task.Delay(15); //Delay to allow UI update

        try
        {
            LoadImmersionWeek(filePath);
        }
        catch(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            CurrentCursor = Cursors.Arrow;
            DropFileMessage = "Drop immersion PDF file here";
            OnPropertyChanged(nameof(CurrentCursor));
            OnPropertyChanged(nameof(DropFileMessage));
        }
    }

    private void LoadImmersionWeek(string filePath)
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
        //VisibilityDropFileTextBlock = Visibility.Collapsed;
        VisibilityImmersionWeekGrid = Visibility.Visible;

        OnPropertyChanged(nameof(MessageTitle));
        OnPropertyChanged(nameof(ImmersionWeekText));
        //OnPropertyChanged(nameof(VisibilityDropFileTextBlock));
        OnPropertyChanged(nameof(VisibilityImmersionWeekGrid));
    }

    public CommandHandler CopyToClipboardCommand => new(() =>
    {
        if (ImmersionWeek is null)
            return;
        
        var textToProjection = immersionExtractor.GetTextToProjection(ImmersionWeek.ImmersionDays);
        Clipboard.SetText(textToProjection);
    });
}
