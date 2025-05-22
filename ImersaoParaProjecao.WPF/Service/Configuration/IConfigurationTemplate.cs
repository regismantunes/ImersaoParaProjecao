using ImmersionToProjection.Model;

namespace ImmersionToProjection.Service.Configuration;

internal interface IConfigurationTemplate
{
    string Language { get; init; }
    IEnumerable<PatternsItem> Patterns { get; init; }
    string Theme { get; init; }
}