namespace ImersaoParaProjecao.Helper
{
    public record RegexHelperPatterns
    {
        public required string ImersionPoints { get; init; }
        public required string EndOfDaillyPoints { get; init; }
        public required string MessageHeader { get; init; }
        public required string Number { get; init; }
        public required string BibleReading { get; init; }
    }
}
