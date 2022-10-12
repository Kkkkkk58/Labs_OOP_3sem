using Isu.Extra.Extensions;

namespace Isu.Extra.Models;

public abstract class LessonScheduler
{
    public void ExpandSchedule(LessonSchedulingOptions options)
    {
        if (LessonDoesNotFitIntoSchedule(options.Lesson.Time, options.ScheduleStart, options.ScheduleEnd))
            throw new NotImplementedException();

        MakeExpansion(options);
    }

    protected abstract void MakeExpansion(LessonSchedulingOptions options);

    private static bool LessonDoesNotFitIntoSchedule(LessonTime lessonTime, DateOnly scheduleStart, DateOnly scheduleEnd)
    {
        return lessonTime.Begin > scheduleStart.ToDateTimeWithSameTime(lessonTime.Begin)
               || lessonTime.End > scheduleEnd.ToDateTimeWithSameTime(lessonTime.End);
    }
}