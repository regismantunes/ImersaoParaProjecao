using ImmersionToProjection.Service.Configuration;
using ImmersionToProjection.Service.DynamicResources;
using ImmersionToProjection.Service.Language;
using ImmersionToProjection.Utility;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace ImmersionToProjection.ViewModel;

public class ConfigurationViewModel(
    IConfiguration configuration,
    IConfigurationUpdater configurationUpdater,
    IThemeManager themeManager
    ) : BaseViewModel
{
    public KeyValuePairItem? Language
    {
        get => Languages.FirstOrDefault(x => x.Key == GetPropertyValue());
        set => SetProppertyValue(value?.Key);
    }

    public KeyValuePairItem? Theme
    {
        get => Themes.FirstOrDefault(x => x.Key == GetPropertyValue());
        set
        {
            var theme = value?.Key;
            SetProppertyValue(theme);
            themeManager.ApplyTheme(theme);
        }
    }

    public string? RegexImmersionPoint
    {
        get => GetPropertyValue();
        set => SetProppertyValue(value);
    }

    public string? RegexEndOfDaillyPoint
    {
        get => GetPropertyValue();
        set => SetProppertyValue(value);
    }

    public string? RegexMessageHeader
    {
        get => GetPropertyValue();
        set => SetProppertyValue(value);
    }

    public string? RegexNumber
    {
        get => GetPropertyValue();
        set => SetProppertyValue(value);
    }

    public string? RegexBibleReading
    {
        get => GetPropertyValue();
        set => SetProppertyValue(value);
    }

    public string? MessageTitleFormat
    {
        get => GetPropertyValue();
        set => SetProppertyValue(value);
    }

    public IEnumerable<KeyValuePairItem> Languages => LanguageKeys.AvailableLanguages.Select(x => new KeyValuePairItem(x.Key, x.Value));

    public IEnumerable<KeyValuePairItem> Themes => LanguageKeys.ComboTheme.Split(';').Select(x =>
    {
        var pairValue = x.Split('=');
        return new KeyValuePairItem(pairValue[0], pairValue[1]);
    });

    private string GetPropertyValue([CallerMemberName] string? propertyName = null)
    {
        if (propertyName is null)
            return string.Empty;

        GetConfigurationProperty(propertyName, out var section, out var configurationPropertyName);

        return section.GetValue<string>(configurationPropertyName) ?? string.Empty;
    }

    private void GetConfigurationProperty(string propertyName, out IConfiguration section, out string configurationPropertyName)
    {
        if (propertyName.StartsWith("Regex"))
        {
            section = configuration.GetSection("Regex");
            configurationPropertyName = propertyName[5..];
        }
        else
        {
            section = configuration;
            configurationPropertyName = propertyName;
        }
    }

    private void SetProppertyValue(string? value, [CallerMemberName] string? propertyName = null)
    {
        if (propertyName is null)
            return;

        ArgumentNullException.ThrowIfNull(value, nameof(value));

        GetConfigurationProperty(propertyName, out var section, out var configurationPropertyName);

        if (section.GetValue<string>(configurationPropertyName) != value)
        {
            section[configurationPropertyName] = value;

            configurationUpdater.UpdateSetting(
                propertyName.StartsWith("Regex") ?
                    string.Concat("Regex:", configurationPropertyName) :
                    configurationPropertyName,
                value);

            OnPropertyChanged(propertyName);
        }
    }
}