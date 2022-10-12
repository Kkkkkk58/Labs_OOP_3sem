namespace Isu.Extra.Models;

public struct LessonSchedulingOptions
{
    public LessonSchedulingOptions(Lesson lesson, ICollection<Lesson> schedule, DateOnly scheduleStart, DateOnly scheduleEnd, int lessonRepeatNumber)
    {
        if (scheduleStart > scheduleEnd)
            throw new NotImplementedException();
        if (lessonRepeatNumber <= 0)
            throw new NotImplementedException();

        Lesson = lesson;
        Schedule = schedule;
        ScheduleStart = scheduleStart;
        ScheduleEnd = scheduleEnd;
        LessonRepeatNumber = lessonRepeatNumber;
    }

    public Lesson Lesson { get; }
    public ICollection<Lesson> Schedule { get; }
    public DateOnly ScheduleStart { get; }
    public DateOnly ScheduleEnd { get; }
    public int LessonRepeatNumber { get; }
}