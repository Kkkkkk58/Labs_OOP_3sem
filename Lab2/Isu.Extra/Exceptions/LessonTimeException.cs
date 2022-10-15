namespace Isu.Extra.Exceptions;

public class LessonTimeException : IsuExtraException
{
    private LessonTimeException(string message)
        : base(message)
    {
    }

    public static LessonTimeException InvalidLessonDates(DateTime begin, DateTime end)
    {
        throw new LessonTimeException($"Invalid lesson time: {begin} - {end}");
    }
}