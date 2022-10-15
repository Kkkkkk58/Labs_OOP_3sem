using Isu.Extra.Entities;
using Isu.Models.IsuInformationDetails;

namespace Isu.Extra.Exceptions;

public class ExtraStreamException : IsuExtraException
{
    private ExtraStreamException(string message)
        : base(message)
    {
    }

    public static ExtraStreamException StudentAlreadyExists(ExtraStream extraStream, IsuId studentId)
    {
        throw new ExtraStreamException($"Student with id={studentId} is already assigned to stream {extraStream.Id}");
    }

    public static ExtraStreamException StreamLimitReached(ExtraStream extraStream, int streamLimit)
    {
        throw new ExtraStreamException($"Stream {extraStream.Id} limit of {streamLimit} students is reached");
    }

    public static ExtraStreamException StudentNotFound(ExtraStream extraStream, IsuId studentId)
    {
        throw new ExtraStreamException($"$Stream {extraStream.Id} doesn't contain student {studentId}");
    }
}