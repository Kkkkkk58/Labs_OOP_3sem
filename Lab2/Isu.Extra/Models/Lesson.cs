using Isu.Extra.Entities;

namespace Isu.Extra.Models;

public record Lesson
{
    public Lesson(string name, LessonTime time, Teacher teacher, ClassRoomLocation classRoomLocation)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Time = time;
        Teacher = teacher ?? throw new ArgumentNullException(nameof(teacher));
        ClassRoomLocation = classRoomLocation;
    }

    public string Name { get; }
    public LessonTime Time { get; init; }
    public Teacher Teacher { get; }
    public ClassRoomLocation ClassRoomLocation { get; }
}