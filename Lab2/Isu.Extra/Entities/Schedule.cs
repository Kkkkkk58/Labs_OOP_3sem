using Isu.Extra.Entities.LessonSchedulers;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class Schedule
{
    private readonly IReadOnlyList<Lesson> _lessons;

    private Schedule(IEnumerable<Lesson> lessons)
    {
        _lessons = lessons
            .OrderBy(lesson => lesson.Time.Begin)
            .ToList()
            .AsReadOnly();
    }

    public static ScheduleBuilder Builder => new ScheduleBuilder();

    public bool HasIntersections(Schedule other)
    {
        ArgumentNullException.ThrowIfNull(other);

        return _lessons
            .Any(curLesson => other._lessons.Any(otherLesson => otherLesson.Time.Intersects(curLesson.Time)));
    }

    public Schedule Combine(Schedule other)
    {
        ArgumentNullException.ThrowIfNull(other);

        if (HasIntersections(other))
            throw ScheduleException.ScheduleCombinationIntersects();

        var newLessons = new List<Lesson>(_lessons);
        newLessons.AddRange(other._lessons);

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
                throw new NotImplementedException();

            _startDate = startDate;
            return this;
        }

        public ScheduleBuilder SetEndDate(DateOnly endDate)
        {
            if (endDate < _startDate)
                throw new NotImplementedException();

            _endDate = endDate;
            return this;
        }

        public ScheduleBuilder AddLesson(ILessonScheduler lessonScheduler, ILessonSchedulingOptionsBuilder optionsBuilder)
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