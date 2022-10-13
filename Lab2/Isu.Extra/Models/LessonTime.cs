using Isu.Extra.Exceptions;

namespace Isu.Extra.Models;

public readonly record struct LessonTime
{
    public LessonTime(DateTime begin, TimeSpan duration)
    {
        Begin = begin;
        Duration = duration;
        End = begin + Duration;

        if (End.Date != Begin.Date)
            throw LessonTimeException.InvalidLessonDates(Begin, End);
    }

    public DateTime Begin { get; }
    public DateTime End { get; }
    public TimeSpan Duration { get; }

    public bool Intersects(LessonTime other)
    {
        return End >= other.Begin && Begin <= other.End;
    }
}