using Isu.Exceptions;
using Isu.Extensions;

namespace Isu.Models.IsuInformationDetails;

public record FacultyLetter
{
    public FacultyLetter(char value)
    {
        if (IsInvalidFacultyLetter(value))
            throw InvalidIsuInformationException.InvalidFacultyLetter(value);

        Value = value;
    }

    public char Value { get; }

    public static FacultyLetter Parse(string s)
    {
        if (!s.TryTransformToLetter(out char letter))
            throw InvalidIsuInformationException.InvalidFacultyLetterParsingArgument(s);

        return new FacultyLetter(letter);
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    private static bool IsInvalidFacultyLetter(char letter)
    {
        return !char.IsLetter(letter);
    }
}