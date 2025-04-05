using ImmersionToProjection.Extensions;
using ImmersionToProjection.Model;

namespace ImmersionToProjection.ViewModel
{
    public class ImmersionDayViewModel(ImmersionDay immersionDay, IFormatProvider formatProvider)
    {
        public string Day => immersionDay.Day.GetLocalizedName(formatProvider);
        public string Items => string.Join("\r\n", immersionDay.Items);
    }
}