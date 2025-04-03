using ImersaoParaProjecao.Extensions;
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
                ImmersionPoint = GetConfigurationValue(configurationRegex, nameof(RegexHelperPatterns.ImmersionPoint)),
                EndOfDaillyPoint = GetConfigurationValue(configurationRegex, nameof(RegexHelperPatterns.EndOfDaillyPoint)),
                MessageHeader = GetConfigurationValue(configurationRegex, nameof(RegexHelperPatterns.MessageHeader)),
                Number = GetConfigurationValue(configurationRegex, nameof(RegexHelperPatterns.Number)),
                BibleReading = GetConfigurationValue(configurationRegex, nameof(RegexHelperPatterns.BibleReading)),
            };
        }

        private static string GetConfigurationValue(IConfiguration configuration, string key)
            => configuration.GetValueValidating<string>(key, $"Missing {key} Regex expression");
    }
}
