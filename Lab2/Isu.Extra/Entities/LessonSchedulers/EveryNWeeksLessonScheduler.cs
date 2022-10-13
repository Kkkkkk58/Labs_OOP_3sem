using Isu.Extra.Exceptions;
using Isu.Extra.Extensions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities.LessonSchedulers;

public class EveryNWeeksLessonScheduler : LessonScheduler
{
    private const int DaysInWeek = 7;
    private readonly int _weeksNumber;

    public EveryNWeeksLessonScheduler(int weeksNumber)
    {
        if (weeksNumber <= 0)
            throw new ArgumentOutOfRangeException(nameof(weeksNumber));

        _weeksNumber = weeksNumber;
    }

    protected override void MakeExpansion(ILessonSchedulingOptions options)
    {
        if (RepeatNumberExceedsNumberOfAvailableWeeks(options))
            throw LessonSchedulerException.RepeatNumberOutOfRange(options.LessonRepeatNumber);

        IEnumerable<Lesson> lessonsToAdd = GetLessons(options.Lesson, options.LessonRepeatNumber, options.Schedule);
        foreach (Lesson lesson in lessonsToAdd)
        {
            options.Schedule.Add(lesson);
        }
    }

    private Lesson GetLessonForNextNthWeek(Lesson curLesson)
    {
        return curLesson with
        {
            Time = new LessonTime(curLesson.Time.Begin + TimeSpan.FromDays(DaysInWeek * _weeksNumber), curLesson.Time.Duration),
        };
    }

    private IEnumerable<Lesson> GetLessons(Lesson lesson, int lessonRepeatNumber, ICollection<Lesson> schedule)
    {
        var lessons = new List<Lesson>();
        Lesson curLesson = lesson;

        for (int i = 0; i < lessonRepeatNumber; ++i)
        {
            lessons.Add(curLesson);
            if (schedule.Any(scheduledLesson => scheduledLesson.Time.Intersects(curLesson.Time)))
                throw new NotImplementedException();

            curLesson = GetLessonForNextNthWeek(curLesson);
        }

        return lessons;
    }

    private bool RepeatNumberExceedsNumberOfAvailableWeeks(ILessonSchedulingOptions options)
    {
        return options.ScheduleEnd.GetDifferenceInWeeks(options.Lesson.Time.End) % _weeksNumber > options.LessonRepeatNumber;
    }
}