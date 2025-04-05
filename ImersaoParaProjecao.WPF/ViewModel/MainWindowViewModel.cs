using ImmersionToProjection.Service.Interfaces;
using ImmersionToProjection.View;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace ImmersionToProjection.ViewModel;

public class MainWindowViewModel(IImmersionExtractor immersionExtractor) : BaseViewModel
{
    public string DropFileMessage { get; private set; } = "Drop immersion PDF file here";
    public ImmersionWeekView? ImmersionWeekView { get; private set; }
    public Visibility VisibilityDropFileTextBlock { get; private set; }
    public Visibility VisibilityImmersionWeekView { get; private set; }
    public Cursor CurrentCursor { get; private set; } = Cursors.Arrow;

    public void OnFileDropped(string filePath)
    {
        CurrentCursor = Cursors.Wait;
        DropFileMessage = "Extracting immersion points...";
        OnPropertyChanged(nameof(CurrentCursor));
        OnPropertyChanged(nameof(DropFileMessage));
        ForceUIUpdate();

        LoadImmersionWeek(filePath).ContinueWith(task =>
        {
            if (task.IsFaulted)
                MessageBox.Show(task.Exception?.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            CurrentCursor = Cursors.Arrow;
            DropFileMessage = "Drop immersion PDF file here";
            OnPropertyChanged(nameof(CurrentCursor));
            OnPropertyChanged(nameof(DropFileMessage));
        });
    }

    private async Task LoadImmersionWeek(string filePath)
    {
        var immersionWeek = immersionExtractor.ExtractFromFile(filePath);
        if (immersionWeek == null)
            return;

        ImmersionWeekView = App.AppHost!.Services.GetRequiredService<ImmersionWeekView>();
        if (ImmersionWeekView.DataContext is ImmersionWeekViewModel immersionWeekViewModel)
            immersionWeekViewModel.SetImmersionWeek(immersionWeek);

        VisibilityDropFileTextBlock = Visibility.Collapsed;
        VisibilityImmersionWeekView = Visibility.Visible;

        OnPropertyChanged(nameof(ImmersionWeekView));
        OnPropertyChanged(nameof(VisibilityDropFileTextBlock));
        OnPropertyChanged(nameof(VisibilityImmersionWeekView));

        await Task.CompletedTask;
    }
}