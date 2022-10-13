namespace Isu.Extra.Exceptions;

public class ExtraCourseException : IsuExtraException
{
    public ExtraCourseException(string message)
        : base(message)
    {
    }

    public static ExtraCourseException EmptyName()
    {
        throw new ExtraCourseException("The name of a course can't be empty");
    }

    public static ExtraCourseException StreamAlreadyExists(Guid courseId, Guid streamId)
    {
        throw new ExtraCourseException($"The course {courseId} already provides stream {streamId}");
    }
}