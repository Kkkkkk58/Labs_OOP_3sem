using Isu.Extra.Models;

namespace Isu.Extra.Entities.LessonSchedulers;

public interface ILessonScheduler
{
    void ExpandSchedule(ILessonSchedulingOptions options);
}