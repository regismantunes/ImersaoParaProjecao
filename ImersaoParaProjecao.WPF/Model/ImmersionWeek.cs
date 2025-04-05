namespace ImmersionToProjection.Model;

public record ImmersionWeek
{
    public required string MessageTitle { get; init; }

    public required IEnumerable<ImmersionDay> ImmersionDays { get; init; }
}
