namespace Isu.Extra.Models;

public interface ILessonSchedulingOptions
{
    Lesson Lesson { get; }
    ICollection<Lesson> Schedule { get; }
    DateOnly ScheduleStart { get; }
    DateOnly ScheduleEnd { get; }
    int LessonRepeatNumber { get; }
}