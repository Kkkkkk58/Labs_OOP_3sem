﻿using Isu.Extra.Exceptions;

namespace Isu.Extra.Models;

public record LessonSchedulingOptions : ILessonSchedulingOptions
{
    public LessonSchedulingOptions(
        Lesson lesson,
        ICollection<Lesson> schedule,
        DateOnly scheduleStart,
        DateOnly scheduleEnd,
        int lessonRepeatNumber)
    {
        if (scheduleStart > scheduleEnd)
            throw LessonSchedulingOptionsException.InvalidScheduleTimeRange(scheduleStart, scheduleEnd);
        if (lessonRepeatNumber <= 0)
            throw new ArgumentOutOfRangeException(nameof(lessonRepeatNumber));

        Lesson = lesson ?? throw new ArgumentNullException(nameof(lesson));
        Schedule = schedule ?? throw new ArgumentNullException(nameof(schedule));
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