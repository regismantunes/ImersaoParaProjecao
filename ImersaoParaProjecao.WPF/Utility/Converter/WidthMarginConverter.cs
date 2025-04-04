using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ImersaoParaProjecao.Utility.Converter;

public class WidthMarginConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values[0] is double actualWidth)
        {
            actualWidth -= 25;

            for (var i = 1; i < values.Length; i++)
                if (values[i] is Thickness margin)
                    actualWidth = actualWidth - margin.Left - margin.Right;
            
            return actualWidth < 0 ? 0 : actualWidth;
        }

        return 0;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}