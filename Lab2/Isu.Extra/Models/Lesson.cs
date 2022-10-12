using Isu.Extra.Entities;

namespace Isu.Extra.Models;

public record Lesson
{
    public Lesson(LessonTime time, Teacher teacher, ClassRoomNumber classRoomNumber)
    {
        Id = Guid.NewGuid();
        Time = time;
        Teacher = teacher;
        ClassRoomNumber = classRoomNumber;
    }

    public Guid Id { get; }
    public LessonTime Time { get; init; }
    public Teacher Teacher { get; }
    public ClassRoomNumber ClassRoomNumber { get; }
}