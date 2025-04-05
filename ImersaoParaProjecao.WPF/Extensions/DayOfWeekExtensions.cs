using System.Globalization;

namespace ImmersionToProjection.Extensions
{
    public static class DayOfWeekExtensions
    {
        public static string GetLocalizedName(this DayOfWeek dayOfWeek, IFormatProvider formatProvider)
        { 
            var dayName = formatProvider is CultureInfo cultureInfo ?
                cultureInfo.DateTimeFormat.GetDayName(dayOfWeek) :
                dayOfWeek.ToString();

            return dayName.ToUpperInvariant();
        }
    }
}
