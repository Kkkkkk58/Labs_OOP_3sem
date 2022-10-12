namespace Isu.Extra.Models;

public class Schedule
{
    private readonly List<Lesson> _lessons;

    private Schedule(List<Lesson> lessons)
    {
        _lessons = lessons;
        SortLessonsByBegin();
    }

    public static ScheduleBuilder Builder => new ScheduleBuilder();

    public bool HasIntersections(Schedule other)
    {
        return _lessons.Any(curLesson => other._lessons.Any(otherLesson => otherLesson.Time.Intersects(curLesson.Time)));
    }

    public Schedule Combine(Schedule other)
    {
        if (HasIntersections(other))
            throw new NotImplementedException();

        var newLessons = new List<Lesson>(_lessons);
        newLessons.AddRange(other._lessons);

        return new Schedule(newLessons);
    }

    private void SortLessonsByBegin()
    {
        _lessons.Sort((lhs, rhs) => (rhs.Time.Begin - lhs.Time.Begin).Seconds);
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

        public ScheduleBuilder AddLesson(Lesson lesson, LessonScheduler lessonScheduler, int repeatNumber)
        {
            if (_startDate is null || _endDate is null)
                throw new NotImplementedException();
            if (_lessons.Contains(lesson))
                throw new NotImplementedException();

            LessonSchedulingOptions options = new LessonSchedulingOptionsBuilder()
                .SetStart(_startDate.Value)
                .SetEnd(_endDate.Value)
                .SetSchedule(_lessons)
                .SetLesson(lesson)
                .SetLessonRepeatNumber(repeatNumber)
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