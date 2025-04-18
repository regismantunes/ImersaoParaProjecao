using ImmersionToProjection.Extensions;
using Microsoft.Extensions.Configuration;

namespace ImmersionToProjection.Service.Extraction.Patterns;

public static class RegexPatternsFactory
{
    public static RegexPatterns CreateFromConfiguration(IConfiguration configuration)
    {
        var configurationRegex = configuration.GetSection("Regex");
        return new RegexPatterns()
        {
            ImmersionPoint = GetConfigurationValue(configurationRegex, nameof(RegexPatterns.ImmersionPoint)),
            EndOfDaillyPoint = GetConfigurationValue(configurationRegex, nameof(RegexPatterns.EndOfDaillyPoint)),
            MessageHeader = GetConfigurationValue(configurationRegex, nameof(RegexPatterns.MessageHeader)),
            Number = GetConfigurationValue(configurationRegex, nameof(RegexPatterns.Number)),
            BibleReading = GetConfigurationValue(configurationRegex, nameof(RegexPatterns.BibleReading)),
        };
    }

    private static string GetConfigurationValue(IConfiguration configuration, string key)
        => configuration.GetValueValidating(key, $"Missing {key} Regex expression");
}
