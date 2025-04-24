namespace ImmersionToProjection.Utility;

public record KeyValuePairItem(string key, string value)
{
    public string Key { get; set; } = key;
    public string Value { get; set; } = value;

    public override string ToString()
    {
        return Value;
    }
}