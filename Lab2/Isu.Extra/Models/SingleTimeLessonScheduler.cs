namespace Isu.Extra.Models;

public class SingleTimeLessonScheduler : LessonScheduler
{
    public SingleTimeLessonScheduler()
    {
    }

    protected override void MakeExpansion(LessonSchedulingOptions options)
    {
        if (options.LessonRepeatNumber != 1)
            throw new NotImplementedException();

        options.Schedule.Add(options.Lesson);
    }
}