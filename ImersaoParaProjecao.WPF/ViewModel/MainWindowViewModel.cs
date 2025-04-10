using ImmersionToProjection.Utility;
using ImmersionToProjection.View;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ImmersionToProjection.ViewModel;

public class MainWindowViewModel(ImmersionWeekView immersionWeekView) : BaseViewModel
{
    public string DropFileMessage 
    { 
        get;
        private set 
        {
            field = value;
            OnPropertyChanged();
        }
    } = "Drop immersion PDF file here";

    public Visibility VisibilityContentUserControl 
    { 
        get;
        private set
        {
            field = value;
            OnPropertyChanged();
        }
    } = Visibility.Collapsed;

    public Cursor CurrentCursor
    { 
        get; 
        private set
        {
            field = value;
            OnPropertyChanged();
        }
    } = Cursors.Arrow;

    private readonly ImmersionWeekView _immersionWeekView = immersionWeekView;
    public UserControl ContentUserControl
    { 
        get;
        private set
        {
            field = value;
            OnPropertyChanged();
        }
    } = immersionWeekView;

    public async Task OnFileDroppedAsync(string filePath)
    {
        CurrentCursor = Cursors.Wait;
        DropFileMessage = "Extracting immersion points...";
        VisibilityContentUserControl = Visibility.Collapsed;
        await Task.Delay(15); //Delay to allow UI update

        try
        {
            ContentUserControl = _immersionWeekView;
            if (_immersionWeekView.DataContext is ImmersionWeekViewModel viewModel)
            {
                viewModel.LoadImmersionWeek(filePath);
                VisibilityContentUserControl = Visibility.Visible;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            CurrentCursor = Cursors.Arrow;
            DropFileMessage = "Drop immersion PDF file here";
        }
    }

    public CommandHandler ConfigurationCommand => new(() =>
    {
        ContentUserControl = App.AppHost!.Services.GetRequiredService<ConfigurationView>();
        VisibilityContentUserControl = Visibility.Visible;
    });
}