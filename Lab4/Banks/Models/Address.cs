namespace Banks.Models;

public record Address
{
    private string _value;

    public Address(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));
        _value = value;
    }
}