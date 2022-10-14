using Isu.Extra.Entities.LessonSchedulers;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class Schedule
{
    private Schedule(IEnumerable<Lesson> lessons)
    {
        Lessons = lessons
            .OrderBy(lesson => lesson.Time.Begin)
            .ToList()
            .AsReadOnly();
    }

    public static ScheduleBuilder Builder => new ScheduleBuilder();
    public IReadOnlyList<Lesson> Lessons { get; }

    public bool HasIntersections(Schedule other)
    {
        ArgumentNullException.ThrowIfNull(other);

        return Lessons
            .Any(curLesson => other.Lessons.Any(otherLesson => otherLesson.Time.Intersects(curLesson.Time)));
    }

    public Schedule Combine(Schedule other)
    {
        ArgumentNullException.ThrowIfNull(other);

        if (HasIntersections(other))
            throw ScheduleException.ScheduleCombinationIntersects();

        var newLessons = new List<Lesson>(Lessons);
        newLessons.AddRange(other.Lessons);

        return new Schedule(newLessons);
    }

    public class ScheduleBuilder
    {
        private readonly List<Lesson> _lessons;
        private DateOnly? _startDate;
        private DateOnly? _endDate;

        public ScheduleBuilder()
        {
            _lessons = new List<Lesson>();
            _startDate = _endDate = null;
        }

        public ScheduleBuilder SetStartDate(DateOnly startDate)
        {
            if (startDate > _endDate)
                throw ScheduleBuilderException.StartDateExceedsEndDate(startDate, _endDate.Value);

            _startDate = startDate;
            return this;
        }

        public ScheduleBuilder SetEndDate(DateOnly endDate)
        {
            if (endDate < _startDate)
                throw ScheduleBuilderException.EndDatePrecedesStartDate(endDate, _startDate.Value);

            _endDate = endDate;
            return this;
        }

        public ScheduleBuilder AddLesson(
            ILessonScheduler lessonScheduler,
            ILessonSchedulingOptionsBuilder optionsBuilder)
        {
            ArgumentNullException.ThrowIfNull(lessonScheduler);
            ArgumentNullException.ThrowIfNull(optionsBuilder);

            if (_startDate is null || _endDate is null)
                throw ScheduleBuilderException.NoTimeLimitsProvided();

            ILessonSchedulingOptions options = optionsBuilder
                .SetStart(_startDate.Value)
                .SetEnd(_endDate.Value)
                .SetSchedule(_lessons)
                .Build();

            lessonScheduler.ExpandSchedule(options);
            return this;
        }

        public Schedule Build()
        {
            return new Schedule(_lessons);
        }
    }
}