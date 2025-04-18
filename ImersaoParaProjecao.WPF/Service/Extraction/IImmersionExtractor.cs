using ImmersionToProjection.Model;

namespace ImmersionToProjection.Service.Extraction;

public interface IImmersionExtractor
{
    ImmersionWeek? ExtractFromFile(string filePath);
    string GetTextToProjection(IEnumerable<ImmersionDay> immersionDays);
}