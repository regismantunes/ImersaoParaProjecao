using ImmersionToProjection.Extensions;
using ImmersionToProjection.Service.Configuration;
using ImmersionToProjection.Service.DynamicResources;
using ImmersionToProjection.Service.Extraction.Patterns;
using ImmersionToProjection.Service.Language;
using ImmersionToProjection.Utility;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace ImmersionToProjection.ViewModel;

public class ConfigurationViewModel : BaseViewModel
{
    private readonly IAppConfiguration _configuration;
    private readonly IThemeManager _themeManager;

    public ConfigurationViewModel(
        ILanguageKeys languageKeys,
        IAppConfiguration configuration,
        IThemeManager themeManager)
        : base(languageKeys)
    {
        configuration.PropertyChanged += Configuration_PropertyChanged;

        ConfigurationPatterns = configuration.Patterns.Select(x =>
        {
            var vm = new ConfigurationPatternsItemViewModel(languageKeys) 
            { 
                PatternsItem = x
            };

            vm.PropertyChanged += (_, _) => 
            {
                configuration.Save();
                OnPropertyChanged(nameof(ConfigurationPatterns));
            };

            return vm;
        }).ToList();

        _configuration = configuration;
        _themeManager = themeManager;
    }

    private void Configuration_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName != "Language")
            return;

        OnPropertyChanged(nameof(ConfigurationViewModel.Language));
        OnPropertyChanged(nameof(ConfigurationViewModel.Theme));
        OnPropertyChanged(nameof(ConfigurationViewModel.Themes));
    }

    public KeyValuePairItem? Language
    {
        get => Languages.FirstOrDefault(x => x.Key == _configuration.Language);
        set => _configuration.Language = value?.Key ?? string.Empty;
    }

    public KeyValuePairItem? Theme
    {
        get => Themes.FirstOrDefault(x => x.Key == _configuration.Theme);
        set
        {
            var theme = value?.Key;
            _configuration.Theme = theme ?? string.Empty;
            _themeManager.ApplyTheme(theme);
        }
    }

    public IEnumerable<ConfigurationPatternsItemViewModel> ConfigurationPatterns { get; private set; }

    public IEnumerable<KeyValuePairItem> Languages => LanguageKeys!.AvailableLanguages.Select(x => new KeyValuePairItem(x.Key, x.Value));

    public IEnumerable<KeyValuePairItem> Themes => LanguageKeys!.ComboTheme.Split(';').Select(x =>
    {
        var pairValue = x.Split('=');
        return new KeyValuePairItem(pairValue[1], pairValue[0]);
    });
}