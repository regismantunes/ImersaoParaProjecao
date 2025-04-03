namespace ImersaoParaProjecao.Model;

public record ImmersionDay
{
    public required DayOfWeek Day { get; init; }

    public required string[] Items { get; init; }
}
