using ImmersionToProjection.Service.Language;
using ImmersionToProjection.Service.ViewFactory;
using ImmersionToProjection.Utility;
using ImmersionToProjection.View;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ImmersionToProjection.ViewModel;

public class MainWindowViewModel(ILanguageKeys languageKeys, IImmersionWeekViewFactory immersionWeekViewFactory) : BaseViewModel(languageKeys)
{
    public Visibility DropFileMessageVisibility
    { 
        get; 
        private set
        {
            field = value;
            OnPropertyChanged();
        }
    } = Visibility.Visible;

    public Visibility ExtractingImmersionPointsMessageVisibility
    {
        get;
        private set
        {
            field = value;
            OnPropertyChanged();
        }
    } = Visibility.Collapsed;

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

    public UserControl? ContentUserControl
    { 
        get;
        private set
        {
            field = value;
            OnPropertyChanged();
        }
    }

    public async Task OnFileDroppedAsync(string filePath)
    {
        CurrentCursor = Cursors.Wait;
        DropFileMessageVisibility = Visibility.Collapsed;
        ExtractingImmersionPointsMessageVisibility = Visibility.Visible;
        VisibilityContentUserControl = Visibility.Collapsed;
        await Task.Delay(15); //Delay to allow UI update

        try
        {
            ContentUserControl = immersionWeekViewFactory.CreateImmersionWeekView(filePath);
            if (ContentUserControl is not null)
                VisibilityContentUserControl = Visibility.Visible;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            CurrentCursor = Cursors.Arrow;
            DropFileMessageVisibility = Visibility.Visible;
            ExtractingImmersionPointsMessageVisibility = Visibility.Collapsed;
        }
    }

    public CommandHandler ConfigurationCommand => new(() =>
    {
        ContentUserControl = App.AppHost!.Services.GetRequiredService<ConfigurationView>();
        VisibilityContentUserControl = Visibility.Visible;
    });
}