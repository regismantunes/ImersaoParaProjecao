using ImmersionToProjection.Service.Configuration;
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace ImmersionToProjection.Service.Language;

public class LanguageKeys : ILanguageKeys
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public IDictionary<string, string> AvailableLanguages =>
        new Dictionary<string, string>
        {
            { "en", "English" },
            { "es", "Espanhõl" },
            { "pt", "Português" }
        };

    public string DefaultLanguage => "en";

    public string ApplicationTitle => GetString();
    public string ButtonCopyToProjection => GetString();
    public string ComboTheme => GetString();
    public string Error => GetString();
    public string ErrorImpossibleIdentifyImmersionText => GetString();
    public string LabelBibleReading => GetString();
    public string LabelConfiguration => GetString();
    public string LabelEndOfDaillyPoint => GetString();
    public string LabelExtractionParameters => GetString();
    public string LabelImmersionPoint => GetString();
    public string LabelLanguage => GetString();
    public string LabelMessageHeader => GetString();
    public string LabelMessageTitleFormat => GetString();
    public string LabelMessageTitleFormatTip => GetString();
    public string LabelNumber => GetString();
    public string LabelTheme => GetString();
    public string MessageDropImmersionPdfFileHere => GetString();
    public string MessageExtractingImmersionPoints => GetString();

    private readonly ResourceManager _resourceManager =
        new ResourceManager("ImmersionToProjection.Resources.Languages.Strings", typeof(LanguageKeys).Assembly);
    private readonly IAppConfiguration _configuration;

    public LanguageKeys(IAppConfiguration configuration)
    {
        _configuration = configuration;

        _configuration.PropertyChanged += _configuration_PropertyChanged;
    }

    private void _configuration_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(IAppConfiguration.Language))
            return;

        OnPropertyChanged(nameof(ApplicationTitle));
        OnPropertyChanged(nameof(ButtonCopyToProjection));
        OnPropertyChanged(nameof(ComboTheme));
        OnPropertyChanged(nameof(Error));
        OnPropertyChanged(nameof(ErrorImpossibleIdentifyImmersionText));
        OnPropertyChanged(nameof(LabelBibleReading));
        OnPropertyChanged(nameof(LabelConfiguration));
        OnPropertyChanged(nameof(LabelEndOfDaillyPoint));
        OnPropertyChanged(nameof(LabelExtractionParameters));
        OnPropertyChanged(nameof(LabelImmersionPoint));
        OnPropertyChanged(nameof(LabelLanguage));
        OnPropertyChanged(nameof(LabelMessageHeader));
        OnPropertyChanged(nameof(LabelMessageTitleFormat));
        OnPropertyChanged(nameof(LabelMessageTitleFormatTip));
        OnPropertyChanged(nameof(LabelNumber));
        OnPropertyChanged(nameof(LabelTheme));
        OnPropertyChanged(nameof(MessageDropImmersionPdfFileHere));
        OnPropertyChanged(nameof(MessageExtractingImmersionPoints));
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private string GetString([CallerMemberName] string? key = null)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(key, nameof(key));

        var language = _configuration.Language;

        if (string.IsNullOrEmpty(language))
            language = DefaultLanguage;

        var culture = CultureInfo.GetCultureInfo(language);

        return _resourceManager.GetString(key, culture) ?? string.Empty;
    }
}