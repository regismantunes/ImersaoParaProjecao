using ImmersionToProjection.Model;

namespace ImmersionToProjection.Service.Interfaces;

public interface IImmersionExtractor
{
    ImmersionWeek? ExtractFromFile(string filePath);
    string GetTextToProjection(IEnumerable<ImmersionDay> immersionDays);
}