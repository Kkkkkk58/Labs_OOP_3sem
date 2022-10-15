using Isu.Extra.Exceptions;

namespace Isu.Extra.Models;

public readonly record struct LessonTime
{
    public LessonTime(DateTime start, TimeSpan duration)
    {
        Start = start;
        Duration = duration;
        End = start + Duration;

        if (End.Date != Start.Date)
            throw LessonTimeException.InvalidLessonDates(Start, End);
    }

    public DateTime Start { get; }
    public DateTime End { get; }
    public TimeSpan Duration { get; }

    public bool Intersects(LessonTime other)
    {
        return End >= other.Start && Start <= other.End;
    }
}