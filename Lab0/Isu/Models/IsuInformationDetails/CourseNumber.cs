using Isu.Exceptions;
using Isu.Extensions;

namespace Isu.Models.IsuInformationDetails;

public record CourseNumber
{
    private const int MinCourse = 1;
    private const int MaxCourse = 4;

    public CourseNumber(int value)
    {
        if (value is < MinCourse or > MaxCourse)
            throw InvalidIsuInformationException.CourseNumberOutOfRange(value);

        Value = value;
    }

    public int Value { get; }

    public static CourseNumber Parse(string s)
    {
        if (!s.TryTransformToNum(out int number))
            throw InvalidIsuInformationException.InvalidCourseNumberParsingArgument(s);

        return new CourseNumber(number);
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}