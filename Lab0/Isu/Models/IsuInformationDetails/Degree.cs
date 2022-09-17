using Isu.Exceptions;
using Isu.Extensions;

namespace Isu.Models.IsuInformationDetails;

public record Degree
{
    public Degree(int numericalCode)
    {
        Type = GetTypeFromNumCode(numericalCode);
        NumericalCode = numericalCode;
    }

    public DegreeType Type { get; }
    public int NumericalCode { get; }

    public static Degree Parse(string s)
    {
        if (!s.TryTransformToNum(out int numCode))
            throw InvalidIsuInformationException.InvalidDegreeParsingArgument(s);

        return new Degree(numCode);
    }

    private static DegreeType GetTypeFromNumCode(int numCode)
    {
        return numCode switch
        {
            3 => DegreeType.Bachelor,
            4 => DegreeType.Master,
            5 => DegreeType.Specialist,
            _ => throw InvalidIsuInformationException.NonexistentDegreeCode(numCode),
        };
    }
}