namespace ImersaoParaProjecao.Model
{
    public class ImmersionWeek
    {
        public required string MessageTitle { get; set; }

        public required IEnumerable<ImmersionDay> ImersionDays { get; set; }
    }
}
