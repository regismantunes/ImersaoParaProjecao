namespace ImmersionToProjection.Service.Extraction.Patterns;

public record RegexPatterns
{
    public required string ImmersionPoint { get; init; }
    public required string EndOfDaillyPoint { get; init; }
    public required string MessageHeader { get; init; }
    public required string Number { get; init; }
    public required string BibleReading { get; init; }
}
