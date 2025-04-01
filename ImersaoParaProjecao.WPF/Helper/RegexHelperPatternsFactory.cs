using Microsoft.Extensions.Configuration;

namespace ImersaoParaProjecao.Helper
{
    public static class RegexHelperPatternsFactory
    {
        public static RegexHelperPatterns CreateFromConfiguration(IConfiguration configuration)
        {
            var configurationRegex = configuration.GetSection("Regex");
            return new RegexHelperPatterns()
            {
                ImersionPoints = GetConfigurationValue(configurationRegex, nameof(RegexHelperPatterns.ImersionPoints)),
                EndOfDaillyPoints = GetConfigurationValue(configurationRegex, nameof(RegexHelperPatterns.EndOfDaillyPoints)),
                MessageHeader = GetConfigurationValue(configurationRegex, nameof(RegexHelperPatterns.MessageHeader)),
                Number = GetConfigurationValue(configurationRegex, nameof(RegexHelperPatterns.Number)),
                BibleReading = GetConfigurationValue(configurationRegex, nameof(RegexHelperPatterns.BibleReading)),
            };
        }

        private static string GetConfigurationValue(IConfiguration configuration, string key)
            => configuration.GetValue<string>(key) ?? throw new MissingFieldException($"Missing {key} Regex expression");
    }
}
