namespace ImersaoParaProjecao.Model.Interfaces
{
    public interface IImersionExtractor
    {
        Dictionary<string, string[]>? GetImersionDays(string filePath, out string? messageTitle);
        string GetImersionToProjection(Dictionary<string, string[]> imersionDays);
    }
}