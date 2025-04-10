using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace ImmersionToProjection.ViewModel
{
    public class ConfigurationViewModel(IConfiguration configuration) : BaseViewModel
    {
        private readonly IConfigurationSection _regexSection = configuration.GetSection("Regex");

        public string? Language
        {
            get => configuration.GetValue<string>("Language");
            set
            {
                if (configuration.GetValue<string>("Language") != value)
                {
                    configuration["Language"] = value;
                    OnPropertyChanged();
                }
            }
        }

        public string? Theme
        {
            get => configuration.GetValue<string>("Theme");
            set
            {
                if (configuration.GetValue<string>("Theme") != value)
                {
                    configuration["Theme"] = value;
                    OnPropertyChanged();
                }
            }
        }

        public string? RegexImmersionPoint
        {
            get => _regexSection.GetValue<string>("ImmersionPoint");
            set
            {
                if (_regexSection.GetValue<string>("ImmersionPoint") != value)
                {
                    _regexSection["ImmersionPoint"] = value;
                    OnPropertyChanged();
                }
            }
        }

        public string? RegexEndOfDaillyPoint
        {
            get => _regexSection.GetValue<string>("EndOfDaillyPoint");
            set
            {
                if (_regexSection.GetValue<string>("EndOfDaillyPoint") != value)
                {
                    _regexSection["EndOfDaillyPoint"] = value;
                    OnPropertyChanged();
                }
            }
        }

        public string? RegexMessageHeader
        {
            get => _regexSection.GetValue<string>("MessageHeader");
            set
            {
                if (_regexSection.GetValue<string>("MessageHeader") != value)
                {
                    _regexSection["MessageHeader"] = value;
                    OnPropertyChanged();
                }
            }
        }

        public string? RegexNumber
        {
            get => _regexSection.GetValue<string>("Number");
            set
            {
                if (_regexSection.GetValue<string>("Number") != value)
                {
                    _regexSection["Number"] = value;
                    OnPropertyChanged();
                }
            }
        }

        public string? RegexBibleReading
        {
            get => _regexSection.GetValue<string>("BibleReading");
            set
            {
                if (_regexSection.GetValue<string>("BibleReading") != value)
                {
                    _regexSection["BibleReading"] = value;
                    OnPropertyChanged();
                }
            }
        }

        public string? MessageTitleFormat
        {
            get => configuration.GetValue<string>("MessageTitleFormat");
            set
            {
                if (configuration.GetValue<string>("MessageTitleFormat") != value)
                {
                    configuration["MessageTitleFormat"] = value;
                    OnPropertyChanged();
                }
            }
        }

        public IEnumerable<string> Languages { get; private set; } = CultureInfo.GetCultures(CultureTypes.AllCultures)
                .Select(c => c.Name);

        public IEnumerable<string> Themes { get; private set; } =
            [
                "Light",
                "Dark"
            ];
    }
}
