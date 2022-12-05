namespace Banks.Models;

public record Address
{
    public Address(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));

        Value = value;
    }

    public string Value { get; }
}