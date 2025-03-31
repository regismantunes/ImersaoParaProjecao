using System.Text.RegularExpressions;

namespace ImersaoParaProjecao.Utility
{
    public static partial class RegexHelper
    {
        [GeneratedRegex(@"^\d+\.")]
        public static partial Regex GetImersionPoints();

        [GeneratedRegex(@"(ANOTE ABAIXO A LUZ NA PALAVRA|ANOTE ABAIXO O SENTIMENTO RECEBIDO DO SENHOR AO IMERGIR NOS PONTOS ACIMA)")]
        public static partial Regex GetEndOfDaillyPoints();

        [GeneratedRegex(@"(\s*Mens\. \d+|\s*Mensagem \d+|Reunião de Abertura)")]
        public static partial Regex GetMessageHeader();

        [GeneratedRegex(@"\d+")]
        public static partial Regex GetNumber();

        [GeneratedRegex(@"Leitura Bíblica")]
        public static partial Regex GetBibleReading();
    }
}
