using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities.LessonSchedulers;

public class SingleTimeLessonScheduler : LessonScheduler
{
    public SingleTimeLessonScheduler()
    {
    }

    protected override void MakeExpansion(ILessonSchedulingOptions options)
    {
        if (options.LessonRepeatNumber != 1)
            throw LessonSchedulerException.InvalidSingleTimeRepeatNumber(options.LessonRepeatNumber);

        options.Schedule.Add(options.Lesson);
    }
}