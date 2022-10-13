using Isu.Models.IsuInformationDetails;

namespace Isu.Extra.Exceptions;

public class StudentDecoratorException : IsuExtraException
{
    private StudentDecoratorException(string message)
        : base(message)
    {
    }

    public static StudentDecoratorException InvalidGroupDecorator(
        IsuId studentId,
        GroupName studentGroupName,
        GroupName decoratorGroupName)
    {
        throw new StudentDecoratorException(
            $"Expected to get decorator for {studentGroupName} of student {studentId} but was {decoratorGroupName}");
    }

    public static StudentDecoratorException ExtraStreamsLimitOutOfRange(int extraStreamsLimit)
    {
        throw new StudentDecoratorException($"Invalid extra streams limit: {extraStreamsLimit}");
    }

    public static StudentDecoratorException StreamsLimitReached(IsuId studentId, int extraStreamsLimit)
    {
        throw new StudentDecoratorException(
            $"Student {studentId} is already enrolled for max number of streams: {extraStreamsLimit}");
    }

    public static StudentDecoratorException AlreadySignedForCourse(IsuId studentId, Guid courseId, Guid extraStreamId)
    {
        throw new StudentDecoratorException(
            $"Can't sign up student {studentId} to stream {extraStreamId}: already enrolled for course {courseId}");
    }

    public static StudentDecoratorException SameCourseMegaFaculty(
        IsuId studentId,
        Guid megaFacultyId,
        Guid extraStreamId)
    {
        throw new StudentDecoratorException(
            $"Can't sign up student {studentId} to stream {extraStreamId}: student is from megaFaculty {megaFacultyId}, course provider");
    }

    public static StudentDecoratorException ScheduleIntersectsWithExtraStream(IsuId studentId, Guid extraStreamId)
    {
        throw new StudentDecoratorException(
            $"Can't sign up student {studentId} to stream {extraStreamId}: schedule intersects with main schedule");
    }
}