using ImmersionToProjection.Model;

namespace ImmersionToProjection.Service.Configuration;

internal record ConfigurationTemplate : IConfigurationTemplate
{
    public required string Language { get; init; }
    public required string Theme { get; init; }
    public required IEnumerable<PatternsItem> Patterns { get; init; }
}
