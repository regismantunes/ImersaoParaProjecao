using ImmersionToProjection.Extensions;

namespace ImmersionToProjection.Service.Formatter;

public class CaseStringFormat(IFormatProvider baseFormatProvider) : ICaseStringFormat
{
    public object? GetFormat(Type formatType)
    {
        return formatType == typeof(ICustomFormatter) ? this : null;
    }

    public string Format(string format, object arg, IFormatProvider formatProvider)
    {
        if (arg is not string str)
            return string.Format(baseFormatProvider, $"{{0:{format}}}", arg);

        return format switch
        {
            "U" => str.ToUpper(),
            "L" => str.ToLower(),
            "Ul" => str.ToUpperFirstLetter(),
            _ => string.Format(baseFormatProvider, $"{{0:{format}}}", arg),
        };
    }
}