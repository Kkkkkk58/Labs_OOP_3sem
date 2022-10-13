namespace Isu.Extra.Exceptions;

public class FacultyException : IsuExtraException
{
    private FacultyException(string message)
        : base(message)
    {
    }

    public static FacultyException EmptyName()
    {
        throw new FacultyException("The name of a faculty can't be empty");
    }
}