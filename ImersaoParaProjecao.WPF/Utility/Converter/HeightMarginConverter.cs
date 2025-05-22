using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ImmersionToProjection.Utility.Converter;

public class HeightMarginConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values[0] is double actualHeight)
        {
            for (var i = 1; i < values.Length; i++)
            {
                if (values[i] is Thickness margin)
                    actualHeight -=  (margin.Top + margin.Bottom);
                else if (values[i] is double fixedValue)
                    actualHeight -= fixedValue;
            }

            return actualHeight < 0 ? 0 : actualHeight;
        }

        return 0;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}