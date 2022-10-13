using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Models.IsuInformationDetails;

namespace Isu.Extra.Exceptions;

public class StudentDecoratorException : IsuExtraException
{
    private StudentDecoratorException(string message)
        : base(message)
    {
    }

    public static StudentDecoratorException InvalidGroupDecorator(IsuId studentId, Group group, GroupDecorator decorator)
    {
        throw new StudentDecoratorException(
            $"Expected to get decorator for {group.Name} of student {studentId} but was {decorator.Name}");
    }

    public static StudentDecoratorException StreamsLimitReached(IsuId studentId, int extraStreamsLimit)
    {
        throw new StudentDecoratorException(
            $"Student {studentId} is already enrolled for max number of streams: {extraStreamsLimit}");
    }

    public static StudentDecoratorException AlreadySignedForCourse(IsuId studentId, ExtraCourse course, ExtraStream extraStream)
    {
        throw new StudentDecoratorException(
            $"Can't sign up student {studentId} to stream {extraStream.Id}: already enrolled for course {course.Id}");
    }

    public static StudentDecoratorException SameCourseMegaFaculty(IsuId studentId, MegaFaculty megaFaculty, ExtraStream extraStream)
    {
        throw new StudentDecoratorException(
            $"Can't sign up student {studentId} to stream {extraStream.Id}: student is from megaFaculty {megaFaculty.Id}, course provider");
    }

    public static StudentDecoratorException ScheduleIntersectsWithExtraStream(IsuId studentId, ExtraStream extraStream)
    {
        throw new StudentDecoratorException(
            $"Can't sign up student {studentId} to stream {extraStream.Id}: schedule intersects with main schedule");
    }

    public static StudentDecoratorException ExtraStreamNotFound(ExtraStream extraStream)
    {
        throw new StudentDecoratorException($"Can't find extra stream {extraStream.Id}");
    }

    public static StudentDecoratorException TransferGroupScheduleHasIntersections(IsuId studentId, Group decoratedGroup)
    {
        throw new StudentDecoratorException($"Unable to transfer student {studentId} to {decoratedGroup.Name}: group schedule intersects with student's extra courses");
    }

    public static StudentDecoratorException TransferGroupHasSameMegaFacultyWithExtraCourse(IsuId studentId, Group decoratedGroup)
    {
        throw new StudentDecoratorException(
            $"Unable to transfer student {studentId} to {decoratedGroup.Name}: group has the same megaFaculty with one of extra courses");
    }
}