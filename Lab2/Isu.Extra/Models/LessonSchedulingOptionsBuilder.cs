namespace Isu.Extra.Models;

public class LessonSchedulingOptionsBuilder
{
    private Lesson? _lesson;
    private ICollection<Lesson>? _schedule;
    private DateOnly? _scheduleStart;
    private DateOnly? _scheduleEnd;
    private int? _lessonRepeatNumber;

    public LessonSchedulingOptionsBuilder()
    {
    }

    public LessonSchedulingOptionsBuilder SetLesson(Lesson lesson)
    {
        _lesson = lesson;
        return this;
    }

    public LessonSchedulingOptionsBuilder SetSchedule(ICollection<Lesson> schedule)
    {
        _schedule = schedule;
        return this;
    }

    public LessonSchedulingOptionsBuilder SetStart(DateOnly scheduleStart)
    {
        _scheduleStart = scheduleStart;
        return this;
    }

    public LessonSchedulingOptionsBuilder SetEnd(DateOnly scheduleEnd)
    {
        _scheduleEnd = scheduleEnd;
        return this;
    }

    public LessonSchedulingOptionsBuilder SetLessonRepeatNumber(int lessonRepeatNumber)
    {
        _lessonRepeatNumber = lessonRepeatNumber;
        return this;
    }

    public LessonSchedulingOptions Build()
    {
        if (_lesson is null)
            throw new NotImplementedException();
        if (_schedule is null)
            throw new NotImplementedException();
        if (_scheduleStart is null || _scheduleEnd is null)
            throw new NotImplementedException();
        _lessonRepeatNumber ??= 1;

        return new LessonSchedulingOptions(_lesson, _schedule, _scheduleStart.Value, _scheduleEnd.Value, _lessonRepeatNumber.Value);
    }
}