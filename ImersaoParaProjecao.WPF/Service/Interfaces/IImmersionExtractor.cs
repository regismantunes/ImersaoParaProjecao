using ImersaoParaProjecao.Model;

namespace ImersaoParaProjecao.Service.Interfaces;

public interface IImmersionExtractor
{
    ImmersionWeek? ExtractFromFile(string filePath);
    string GetTextToProjection(IEnumerable<ImmersionDay> immersionDays);
}