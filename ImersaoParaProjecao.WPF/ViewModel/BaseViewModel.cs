using ImmersionToProjection.Service.Language;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ImmersionToProjection.ViewModel;

public abstract class BaseViewModel : INotifyPropertyChanged
{
    public BaseViewModel(ILanguageKeys? languageKeys)
    {
        languageKeys?.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == null)
                OnPropertyChanged(nameof(LanguageKeys));
        };

        LanguageKeys = languageKeys;
    }

    public ILanguageKeys? LanguageKeys { get; }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}