using ImersaoParaProjecao.Helper.Interfaces;
using System.Text.RegularExpressions;

namespace ImersaoParaProjecao.Helper;

public partial class RegexHelper : IRegexHelper
{
    [GeneratedRegex(@"^\d+\.")]
    public partial Regex GetImersionPoints();

    [GeneratedRegex(@"(ANOTE ABAIXO A LUZ NA PALAVRA|ANOTE ABAIXO O SENTIMENTO RECEBIDO DO SENHOR AO IMERGIR NOS PONTOS ACIMA)")]
    public partial Regex GetEndOfDaillyPoints();

    [GeneratedRegex(@"(\s*Mens\. \d+ |Reunião de Abertura)")]
    public partial Regex GetMessageTitle();
}
