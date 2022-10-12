using Isu.Extra.Extensions;

namespace Isu.Extra.Models;

public class ByWeeksLessonScheduler : LessonScheduler
{
    protected override void MakeExpansion(LessonSchedulingOptions options)
    {
        if (InitialLessonIsNotOnOddWeek(options.Lesson.Time.Begin, options.ScheduleStart))
            throw new NotImplementedException();

        if (RepeatNumberExceedsNumberOfAvailableWeeks(options))
            throw new NotImplementedException();

        IEnumerable<Lesson> lessonsToAdd = GetLessons(options.Lesson, options.LessonRepeatNumber, options.Schedule);
        foreach (Lesson lesson in lessonsToAdd)
        {
            options.Schedule.Add(lesson);
        }
    }

    private static Lesson GetLessonForNextWeek(Lesson curLesson)
    {
        return curLesson with
        {
            Time = new LessonTime(curLesson.Time.Begin + TimeSpan.FromDays(7), curLesson.Time.Duration),
        };
    }

    private static IEnumerable<Lesson> GetLessons(Lesson lesson, int lessonRepeatNumber, ICollection<Lesson> schedule)
    {
        var lessons = new List<Lesson>();
        Lesson curLesson = lesson;

        for (int i = 0; i < lessonRepeatNumber; ++i)
        {
            lessons.Add(curLesson);
            if (schedule.Any(scheduledLesson => scheduledLesson.Time.Intersects(curLesson.Time)))
                throw new NotImplementedException();

            curLesson = GetLessonForNextWeek(curLesson);
        }

        return lessons;
    }

    private static bool RepeatNumberExceedsNumberOfAvailableWeeks(LessonSchedulingOptions options)
    {
        return -options.Lesson.Time.End.GetDifferenceInWeeks(options.ScheduleEnd) % 2 > options.LessonRepeatNumber;
    }

    private static bool InitialLessonIsNotOnOddWeek(DateTime lessonBegin, DateOnly scheduleStart)
    {
        return lessonBegin.GetDifferenceInWeeks(scheduleStart) % 2 == 0;
    }
}