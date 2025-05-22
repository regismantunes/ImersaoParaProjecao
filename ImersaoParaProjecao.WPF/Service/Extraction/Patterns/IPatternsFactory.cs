
using ImmersionToProjection.Model;

namespace ImmersionToProjection.Service.Extraction.Patterns
{
    public interface IPatternsFactory
    {
        IEnumerable<PatternsItem> GetFromConfiguration();
    }
}