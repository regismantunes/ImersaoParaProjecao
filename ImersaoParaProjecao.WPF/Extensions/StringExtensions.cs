using System.Text;

namespace ImersaoParaProjecao.Extensions;

public static class StringExtensions
{
    public static string ToUpperFirstLetter(this string text)
    {
        var lowCaseWords = new[] { "a", "o", "e", "da", "do", "de", "em", "no", "na", "as", "os", "à", "ao", "por", "pela", "pelo", "às", "com", "sem", "nos" };
        var sb = new StringBuilder();
        var words = text.Split(' ');
        for (var i = 0; i < words.Length; i++)
        {
            var word = words[i].ToLower();
            if (string.IsNullOrEmpty(word))
                continue;

            if (sb.Length > 0 && lowCaseWords.Contains(word))
                sb.Append(word);
            else
            {
                var subWords = word.Split('-');
                for (var j = 0; j < subWords.Length; j++)
                {
                    var formatedWord = char.ToUpper(subWords[j][0]) + subWords[j][1..];
                    sb.Append(formatedWord);
                    if (j < subWords.Length - 1)
                        sb.Append('-');
                }
            }
            if (i < words.Length - 1)
                sb.Append(' ');
        }

        return sb.ToString();
    }
}