using Isu.Exceptions;
using Isu.Extensions;

namespace Isu.Models.IsuInformationDetails;

public record GroupNumber
{
    private const int MinNumber = 0;
    private const int MaxNumber = 99;

    public GroupNumber(int value)
    {
        if (value is < MinNumber or > MaxNumber)
            throw InvalidIsuInformationException.GroupNumberOutOfRange(value);

        Value = value;
    }

    public int Value { get; }

    public static GroupNumber Parse(string s)
    {
        if (!s.TryTransformToNum(out int number))
            throw InvalidIsuInformationException.InvalidGroupNumberParsingArgument(s);

        return new GroupNumber(number);
    }

    public override string ToString()
    {
        return Value < 10
            ? $"0{Value}"
            : $"{Value}";
    }
}