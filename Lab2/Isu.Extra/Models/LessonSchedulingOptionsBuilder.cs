using Isu.Extra.Exceptions;

namespace Isu.Extra.Models;

public class LessonSchedulingOptionsBuilder : ILessonSchedulingOptionsBuilder
{
    private Lesson? _lesson;
    private ICollection<Lesson>? _schedule;
    private DateOnly? _scheduleStart;
    private DateOnly? _scheduleEnd;
    private int? _lessonRepeatNumber;

    public LessonSchedulingOptionsBuilder()
    {
    }

    public ILessonSchedulingOptionsBuilder SetLesson(Lesson lesson)
    {
        _lesson = lesson;
        return this;
    }

    public ILessonSchedulingOptionsBuilder SetSchedule(ICollection<Lesson> schedule)
    {
        _schedule = schedule;
        return this;
    }

    public ILessonSchedulingOptionsBuilder SetStart(DateOnly scheduleStart)
    {
        _scheduleStart = scheduleStart;
        return this;
    }

    public ILessonSchedulingOptionsBuilder SetEnd(DateOnly scheduleEnd)
    {
        _scheduleEnd = scheduleEnd;
        return this;
    }

    public ILessonSchedulingOptionsBuilder SetLessonRepeatNumber(int lessonRepeatNumber)
    {
        _lessonRepeatNumber = lessonRepeatNumber;
        return this;
    }

    public ILessonSchedulingOptions Build()
    {
        if (_lesson is null)
            throw LessonSchedulingOptionsBuilderException.UnsetLesson();
        if (_schedule is null)
            throw LessonSchedulingOptionsBuilderException.UnsetSchedule();
        if (_scheduleStart is null || _scheduleEnd is null)
            throw LessonSchedulingOptionsBuilderException.UnsetTimeRange();
        _lessonRepeatNumber ??= 1;

        return new LessonSchedulingOptions(
            _lesson,
            _schedule,
            _scheduleStart.Value,
            _scheduleEnd.Value,
            _lessonRepeatNumber.Value);
    }
}