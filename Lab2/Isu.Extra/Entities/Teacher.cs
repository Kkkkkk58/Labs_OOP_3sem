using Isu.Extra.Exceptions;

namespace Isu.Extra.Entities;

public class Teacher : IEquatable<Teacher>
{
    public Teacher(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw TeacherException.EmptyName();

        Id = Guid.NewGuid();
        Name = name;
    }

    public Guid Id { get; }
    public string Name { get; }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Teacher);
    }

    public bool Equals(Teacher? other)
    {
        return other is not null && Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}