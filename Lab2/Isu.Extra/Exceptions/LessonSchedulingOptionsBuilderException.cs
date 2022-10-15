namespace Isu.Extra.Exceptions;

public class LessonSchedulingOptionsBuilderException : IsuExtraException
{
    private LessonSchedulingOptionsBuilderException(string message)
        : base(message)
    {
    }

    public static LessonSchedulingOptionsBuilderException UnsetLesson()
    {
        throw new LessonSchedulingOptionsBuilderException("Lesson was not set");
    }

    public static LessonSchedulingOptionsBuilderException UnsetSchedule()
    {
        throw new LessonSchedulingOptionsBuilderException("Schedule was not set");
    }

    public static LessonSchedulingOptionsBuilderException UnsetTimeRange()
    {
        throw new LessonSchedulingOptionsBuilderException("Time range was not set");
    }
}