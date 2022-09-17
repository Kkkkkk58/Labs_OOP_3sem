using Isu.Exceptions;

namespace Isu.Models.IsuInformationDetails;

public record IsuId
{
    private const int MinId = 100_000;
    private const int MaxId = 999_999;

    public IsuId(int value)
    {
        if (value is < MinId or > MaxId)
            throw InvalidIsuInformationException.IsuIdOutOfRange(value);

        Value = value;
    }

    public int Value { get; }

    public override string ToString()
    {
        return Value.ToString();
    }
}