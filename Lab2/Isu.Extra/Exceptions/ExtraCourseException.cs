using Isu.Extra.Entities;

namespace Isu.Extra.Exceptions;

public class ExtraCourseException : IsuExtraException
{
    public ExtraCourseException(string message)
        : base(message)
    {
    }

    public static ExtraCourseException StreamAlreadyExists(ExtraCourse course, ExtraStream stream)
    {
        throw new ExtraCourseException($"The course {course.Id} already provides stream {stream.Id}");
    }

    public static ExtraCourseException StreamBelongsToOtherExtraCourse(ExtraStream stream, ExtraCourse curCourse, ExtraCourse otherCourse)
    {
        throw new ExtraCourseException(
            $"Can't add stream {stream.Id} to course {curCourse.Id} because it already belongs to thes course {otherCourse.Id}");
    }
}