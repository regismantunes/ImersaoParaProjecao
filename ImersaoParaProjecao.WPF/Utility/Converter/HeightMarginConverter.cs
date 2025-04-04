using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ImersaoParaProjecao.Utility.Converter;

public class HeightMarginConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values[0] is double actualHeight)
        {
            actualHeight -= 15;

            for (var i = 1; i < values.Length; i++)
                if (values[i] is Thickness margin)
                    actualHeight = actualHeight - margin.Top - margin.Bottom;
            
            return actualHeight < 0 ? 0 : actualHeight;
        }

        return 0;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}