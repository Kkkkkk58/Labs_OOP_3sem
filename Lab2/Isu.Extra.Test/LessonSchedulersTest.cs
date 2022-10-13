using Isu.Extra.Entities;
using Isu.Extra.Entities.LessonSchedulers;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Xunit;

namespace Isu.Extra.Test;

public class LessonSchedulersTest
{
    private const int DaysInWeek = 7;
    private readonly Lesson _lesson;
    private readonly List<Lesson> _lessons;
    private readonly ILessonSchedulingOptionsBuilder _optionsBuilder;

    public LessonSchedulersTest()
    {
        var lessonTime = new LessonTime(
            new DateTime(2022, 11, 10, 13, 0, 0),
            TimeSpan.FromMinutes(90));

        var teacher = new Teacher("Andy F.");
        _lesson = new Lesson("English", lessonTime, teacher, new ClassRoomLocation("Somewhere"));
        _lessons = new List<Lesson>();

        _optionsBuilder = new LessonSchedulingOptionsBuilder()
            .SetStart(new DateOnly(2021, 9, 1))
            .SetEnd(new DateOnly(2023, 9, 1))
            .SetLesson(_lesson)
            .SetSchedule(_lessons);
    }

    [Fact]
    public void SingleTimeScheduling_ListContainsSingleLesson()
    {
        ILessonSchedulingOptions options = _optionsBuilder.Build();
        ILessonScheduler scheduler = new SingleTimeLessonScheduler();

        scheduler.ExpandSchedule(options);
        Assert.Single(_lessons);
        Assert.Contains(_lesson, _lessons);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void EveryNWeeksScheduling_MultipleLessonsWithNWeeksDiff(int weeksNumber)
    {
        const int lessonRepeatNumber = 3;
        ILessonSchedulingOptions options = _optionsBuilder
            .SetLessonRepeatNumber(lessonRepeatNumber)
            .Build();
        ILessonScheduler scheduler = new EveryNWeeksLessonScheduler(weeksNumber);

        scheduler.ExpandSchedule(options);
        Assert.Equal(lessonRepeatNumber, _lessons.Count);
        Assert.True(HasNWeeksDiff(_lessons, weeksNumber));
    }

    [Fact]
    public void LessonRepeatNumberExceedsGivenRange_ThrowException()
    {
        const int lessonRepeatNumber = 100;
        ILessonSchedulingOptions options = _optionsBuilder
            .SetLessonRepeatNumber(lessonRepeatNumber)
            .Build();
        ILessonScheduler scheduler = new EveryNWeeksLessonScheduler(3);

        Assert.Throws<LessonSchedulerException>(() => scheduler.ExpandSchedule(options));
    }

    private static bool HasNWeeksDiff(IReadOnlyList<Lesson> lessons, int n)
    {
        for (int i = 0; i < lessons.Count - 1; ++i)
        {
            if (lessons[i + 1].Time.Begin - lessons[i].Time.Begin != TimeSpan.FromDays(DaysInWeek * n))
                return false;
        }

        return true;
    }
}