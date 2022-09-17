using Isu.Exceptions;

namespace Isu.Models.IsuInformationDetails;

public readonly struct GroupNameDetails
{
    private const int MinParsingLength = 5;
    private const int MaxParsingLength = 6;

    public GroupNameDetails(
        FacultyLetter facultyLetter,
        Degree degree,
        CourseNumber courseNumber,
        GroupNumber groupNumber,
        SpecializationNumber? specializationNumber)
    {
        FacultyLetter = facultyLetter;
        Degree = degree;
        CourseNumber = courseNumber;
        GroupNumber = groupNumber;
        SpecializationNumber = specializationNumber;
    }

    public FacultyLetter FacultyLetter { get; }
    public Degree Degree { get; }
    public CourseNumber CourseNumber { get; }
    public GroupNumber GroupNumber { get; }
    public SpecializationNumber? SpecializationNumber { get; }

    public static GroupNameDetails Parse(string s)
    {
        if (HasInvalidLength(s))
            throw InvalidIsuInformationException.InvalidGroupNameDetailsParsingArgument(s);

        FacultyLetter facultyLetter = ParseFacultyLetter(s);
        Degree degree = ParseDegree(s);
        CourseNumber courseNumber = ParseCourseNumber(s);
        GroupNumber groupNumber = ParseGroupNumber(s);
        SpecializationNumber? specializationNumber = ParseSpecializationNumber(s);

        return new GroupNameDetails(facultyLetter, degree, courseNumber, groupNumber, specializationNumber);
    }

    private static bool HasInvalidLength(string s)
    {
        return s.Length is < MinParsingLength or > MaxParsingLength;
    }

    private static FacultyLetter ParseFacultyLetter(string s)
    {
        return FacultyLetter.Parse(s[..1]);
    }

    private static Degree ParseDegree(string s)
    {
        return Degree.Parse(s[1..2]);
    }

    private static CourseNumber ParseCourseNumber(string s)
    {
        return CourseNumber.Parse(s[2..3]);
    }

    private static GroupNumber ParseGroupNumber(string s)
    {
        return GroupNumber.Parse(s[3..5]);
    }

    private static SpecializationNumber? ParseSpecializationNumber(string s)
    {
        return s.Length < MaxParsingLength ? null : SpecializationNumber.Parse(s[5..6]);
    }
}