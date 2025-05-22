using ImmersionToProjection.Model;
using ImmersionToProjection.Service.Configuration;

namespace ImmersionToProjection.Service.Extraction.Patterns;

public class PatternsFactory(IAppConfiguration configuration) : IPatternsFactory
{
    public IEnumerable<PatternsItem> GetFromConfiguration()
    {
        return configuration.Patterns;
    }
}