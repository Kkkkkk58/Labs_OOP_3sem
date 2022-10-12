namespace Isu.Extra.Models;

public record LessonTime
{
    public LessonTime(DateTime begin, TimeSpan duration)
    {
        Begin = begin;
        Duration = duration;
        End = begin + Duration;

        if (End.Date != Begin.Date)
            throw new NotImplementedException();
    }

    public DateTime Begin { get; }
    public DateTime End { get; }
    public TimeSpan Duration { get; }

    public bool Intersects(LessonTime other)
    {
        return End >= other.Begin && Begin <= other.End;
    }
}