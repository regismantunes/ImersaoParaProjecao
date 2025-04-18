using ImmersionToProjection.Service.Configuration;
using ImmersionToProjection.Service.DynamicResources;
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
    public string? Language
    {
        get => GetPropertyValue();
        set => SetProppertyValue(value);
    }

    public string? Theme
    {
        get => GetPropertyValue();
        set
        {
            SetProppertyValue(value);
            themeManager.ApplyTheme(value);
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

    public IEnumerable<string> Languages { get; private set; } =
        [
            "Português",
            "Espanhõl",
            "English",
            "Korean",
        ];

    public IEnumerable<string> Themes { get; private set; } =
        [
            "Light",
            "Dark"
        ];

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

        if (value is null)
            throw new InvalidOperationException("Value can't be null.");

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