namespace ImersaoParaProjecao.Helper
{
    public record RegexHelperPatterns
    {
        public required string ImmersionPoint { get; init; }
        public required string EndOfDaillyPoint { get; init; }
        public required string MessageHeader { get; init; }
        public required string Number { get; init; }
        public required string BibleReading { get; init; }
    }
}
