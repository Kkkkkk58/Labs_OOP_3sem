using Isu.Extra.Exceptions;
using Isu.Extra.Extensions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities.LessonSchedulers;

public abstract class LessonScheduler : ILessonScheduler
{
    public void ExpandSchedule(ILessonSchedulingOptions options)
    {
        if (LessonDoesNotFitIntoSchedule(options.Lesson.Time, options.ScheduleStart, options.ScheduleEnd))
        {
            throw LessonSchedulerException.LessonTimeOutOfSchedule(
                options.Lesson.Time,
                options.ScheduleStart,
                options.ScheduleEnd);
        }

        MakeExpansion(options);
    }

    protected abstract void MakeExpansion(ILessonSchedulingOptions options);

    private static bool LessonDoesNotFitIntoSchedule(
        LessonTime lessonTime,
        DateOnly scheduleStart,
        DateOnly scheduleEnd)
    {
        return lessonTime.Begin < scheduleStart.ToDateTimeWithSameTime(lessonTime.Begin)
               || lessonTime.End > scheduleEnd.ToDateTimeWithSameTime(lessonTime.End);
    }
}