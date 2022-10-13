namespace Isu.Extra.Exceptions;

public class LessonSchedulingOptionsException : IsuExtraException
{
    private LessonSchedulingOptionsException(string message)
        : base(message)
    {
    }

    public static LessonSchedulingOptionsException InvalidScheduleTimeRange(DateOnly scheduleStart, DateOnly scheduleEnd)
    {
        throw new LessonSchedulingOptionsException($"Invalid time range: {scheduleStart} - {scheduleEnd}");
    }
}