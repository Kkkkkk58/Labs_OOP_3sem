namespace Isu.Extra.Models;

public interface ILessonSchedulingOptionsBuilder
{
    ILessonSchedulingOptionsBuilder SetLesson(Lesson lesson);
    ILessonSchedulingOptionsBuilder SetSchedule(ICollection<Lesson> schedule);
    ILessonSchedulingOptionsBuilder SetStart(DateOnly scheduleStart);
    ILessonSchedulingOptionsBuilder SetEnd(DateOnly scheduleEnd);
    ILessonSchedulingOptionsBuilder SetLessonRepeatNumber(int lessonRepeatNumber);
    ILessonSchedulingOptions Build();
}