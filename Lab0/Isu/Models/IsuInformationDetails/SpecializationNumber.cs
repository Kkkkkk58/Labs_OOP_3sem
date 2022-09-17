using Isu.Exceptions;
using Isu.Extensions;

namespace Isu.Models.IsuInformationDetails;

public record SpecializationNumber
{
    private const int MinSpecialization = 1;
    private const int MaxSpecialization = 2;

    public SpecializationNumber(int value)
    {
        if (value is < MinSpecialization or > MaxSpecialization)
            throw InvalidIsuInformationException.SpecializationOutOfRange(value);

        Value = value;
    }

    public int Value { get; }

    public static SpecializationNumber Parse(string s)
    {
        if (!s.TryTransformToNum(out int number))
            throw InvalidIsuInformationException.InvalidSpecializationNumberParsingArgument(s);

        return new SpecializationNumber(number);
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}