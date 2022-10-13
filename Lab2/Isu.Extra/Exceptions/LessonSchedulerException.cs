using Isu.Extra.Models;

namespace Isu.Extra.Exceptions;

public class LessonSchedulerException : IsuExtraException
{
    private LessonSchedulerException(string message)
        : base(message)
    {
    }

    public static LessonSchedulerException LessonTimeOutOfSchedule(
        LessonTime lessonTime,
        DateOnly scheduleStart,
        DateOnly scheduleEnd)
    {
        throw new LessonSchedulerException(
            $"Lesson time: {lessonTime.Begin}-{lessonTime.End} was out of range: {scheduleStart}-{scheduleEnd}");
    }

    public static LessonSchedulerException InvalidSingleTimeRepeatNumber(int lessonRepeatNumber)
    {
        throw new LessonSchedulerException($"Lesson repeat number expected to be 1 but was {lessonRepeatNumber}");
    }

    public static LessonSchedulerException RepeatNumberOutOfRange(int lessonRepeatNumber)
    {
        throw new LessonSchedulerException($"Lesson's repeat number was out of range: {lessonRepeatNumber}");
    }

    public static LessonSchedulerException IntersectsWithGivenSchedule()
    {
        throw new LessonSchedulerException("Can't perform scheduling: intersections will appear in the schedule");
    }
}