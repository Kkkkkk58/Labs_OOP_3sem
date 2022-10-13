namespace Isu.Extra.Exceptions;

public class TeacherException : IsuExtraException
{
    private TeacherException(string message)
        : base(message)
    {
    }

    public static TeacherException EmptyName()
    {
        throw new TeacherException("Teacher's name can't be empty");
    }
}