using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace ImmersionToProjection.Service.Language;

public class LanguageKeys : ILanguageKeys
{
    public IDictionary<string, string> AvailableLanguages =>
        new Dictionary<string, string>
        {
            { "en-US", "English" },
            { "es-AR", "Espanhõl" },
            { "pt-BR", "Português" }
        };

    public string ApplicationTitle => GetString();
    public string ButtonCopyToProjection => GetString();
    public string ComboTheme => GetString();
    public string Configuration_Language => GetString();
    public string Configuration_MessageTitleFormat => GetString();
    public string Configuration_Regex_BibleReading => GetString();
    public string Configuration_Regex_EndOfDaillyPoint => GetString();
    public string Configuration_Regex_ImmersionPoint => GetString();
    public string Configuration_Regex_MessageHeader => GetString();
    public string Configuration_Regex_Number => GetString();
    public string Configuration_Theme => GetString();
    public string Error => GetString();
    public string ErrorImpossibleIdentifyImmersionText => GetString();
    public string LabelBibleReading => GetString();
    public string LabelConfiguration => GetString();
    public string LabelEndOfDaillyPoint => GetString();
    public string LabelExtractionParameters => GetString();
    public string LabelImmersionPoint => GetString();
    public string LabelLanguage => GetString();
    public string LabelMessageHeader => GetString();
    public string LabelMessageTtitleFormat => GetString();
    public string LabelNumber => GetString();
    public string LabelTheme => GetString();
    public string MessageDropImmersionPdfFileHere => GetString();
    public string MessageExtractingImmersionPoints => GetString();

    private readonly ResourceManager _resourceManager =
        new ResourceManager("ImmersionToProjection.Resources.Languages.Strings", typeof(LanguageKeys).Assembly);

    private string GetString([CallerMemberName] string? key = null)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(key, nameof(key));

        return _resourceManager.GetString(key, CultureInfo.CurrentUICulture) ?? string.Empty;
    }
}