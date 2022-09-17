namespace Isu.Exceptions;

public class InvalidIsuInformationException : IsuException
{
    private InvalidIsuInformationException(string message)
        : base(message)
    {
    }

    public static InvalidIsuInformationException CourseNumberOutOfRange(int courseNumber)
    {
        return new InvalidIsuInformationException($"Course number out of range: {courseNumber}");
    }

    public static InvalidIsuInformationException InvalidCourseNumberParsingArgument(string s)
    {
        return InvalidParsingArgument("course number", s);
    }

    public static InvalidIsuInformationException NonexistentDegreeCode(int numCode)
    {
        return new InvalidIsuInformationException($"Degree with code {numCode} doesn't exist");
    }

    public static InvalidIsuInformationException InvalidDegreeParsingArgument(string s)
    {
        return InvalidParsingArgument("degree", s);
    }

    public static InvalidIsuInformationException InvalidFacultyLetter(char facultyLetter)
    {
        return new InvalidIsuInformationException($"Invalid faculty letter: {facultyLetter}");
    }

    public static InvalidIsuInformationException InvalidFacultyLetterParsingArgument(string s)
    {
        return InvalidParsingArgument("faculty letter", s);
    }

    public static InvalidIsuInformationException InvalidGroupName(string name)
    {
        return new InvalidIsuInformationException($"Invalid group name: {name}");
    }

    public static Exception InvalidGroupNameDetailsParsingArgument(string s)
    {
        return InvalidParsingArgument("group name details", s);
    }

    public static InvalidIsuInformationException GroupNumberOutOfRange(int value)
    {
        return new InvalidIsuInformationException($"Group number is out of range: {value}");
    }

    public static InvalidIsuInformationException InvalidGroupNumberParsingArgument(string s)
    {
        return InvalidParsingArgument("group number", s);
    }

    public static InvalidIsuInformationException IsuIdOutOfRange(int id)
    {
        return new InvalidIsuInformationException($"Isu ID is out of range: {id}");
    }

    public static InvalidIsuInformationException EmptyNameOfPerson()
    {
        return new InvalidIsuInformationException("Person's name can't be empty");
    }

    public static InvalidIsuInformationException SpecializationOutOfRange(int specialization)
    {
        return new InvalidIsuInformationException($"Specialization number is out of range: {specialization}");
    }

    public static InvalidIsuInformationException InvalidSpecializationNumberParsingArgument(string s)
    {
        return InvalidParsingArgument("specialization number", s);
    }

    private static InvalidIsuInformationException InvalidParsingArgument(string objToParse, string from)
    {
        return new InvalidIsuInformationException($"Unable to parse {objToParse} from {from}");
    }
}