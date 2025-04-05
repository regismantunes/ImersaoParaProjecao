using Microsoft.Extensions.Configuration;

namespace ImmersionToProjection.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetValueValidating(this IConfiguration configuration, string key, string? missingExceptionMessage = null)
        {
            var value = configuration.GetValue<string>(key);
            return value ?? throw new MissingFieldException(missingExceptionMessage ?? $"Missing {key} configuration value");
        }
    }
}
