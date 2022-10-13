using Isu.Models.IsuInformationDetails;

namespace Isu.Extra.Exceptions;

public class ExtraStreamException : IsuExtraException
{
    private ExtraStreamException(string message)
        : base(message)
    {
    }

    public static ExtraStreamException EmptyName()
    {
        throw new ExtraStreamException("The name of an extra stream can't be empty");
    }

    public static ExtraStreamException StudentAlreadyExists(Guid extraStreamId, IsuId studentId)
    {
        throw new ExtraStreamException($"Student with id={studentId} is already assigned to stream {extraStreamId}");
    }

    public static ExtraStreamException StreamLimitReached(Guid extraStreamId, int streamLimit)
    {
        throw new ExtraStreamException($"Stream {extraStreamId} limit of {streamLimit} students is reached");
    }

    public static ExtraStreamException StudentNotFound(Guid extraStreamId, IsuId studentId)
    {
        throw new ExtraStreamException($"$Stream {extraStreamId} doesn't contain student {studentId}");
    }

    public static ExtraStreamException StreamBelongsToOtherExtraCourse(Guid courseId, Guid streamId, Guid otherCourseId)
    {
        throw new ExtraStreamException($"Can't add stream {streamId} to course {courseId} because it belongs to course {otherCourseId}");
    }
}