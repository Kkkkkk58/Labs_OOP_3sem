using Shops.Exceptions;

namespace Shops.Models;

public record Address
{
    public Address(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw AddressException.EmptyAddressException();

        Value = value.ToUpperInvariant();
    }

    public string Value { get; }
}