namespace Isu.Extra.Exceptions;

public class ClassRoomLocationException : IsuExtraException
{
    public ClassRoomLocationException(string message)
        : base(message)
    {
    }

    public static ClassRoomLocationException EmptyValue()
    {
        throw new ClassRoomLocationException("Classroom location can't be empty");
    }
}