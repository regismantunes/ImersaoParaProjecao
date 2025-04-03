using Microsoft.Extensions.Configuration;

namespace ImersaoParaProjecao.Extensions
{
    public static class ConfigurationExtensions
    {
        public static T GetValueValidating<T>(this IConfiguration configuration, string key, string? messingExceptionMessage = null)
        {
            var value = configuration.GetValue<T>(key);
            return value ?? throw new MissingFieldException(messingExceptionMessage ?? $"Missing {key} configuration value");
        }
    }
}
