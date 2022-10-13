using Isu.Extra.Exceptions;

namespace Isu.Extra.Entities;

public class MegaFaculty : IEquatable<MegaFaculty>
{
    private readonly List<Faculty> _faculties;

    public MegaFaculty(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentOutOfRangeException(nameof(name));

        Id = Guid.NewGuid();
        Name = name;
        _faculties = new List<Faculty>();
    }

    public Guid Id { get; }
    public string Name { get; }
    public IReadOnlyList<Faculty> Faculties => _faculties;

    public Faculty AddFaculty(Faculty faculty)
    {
        ArgumentNullException.ThrowIfNull(faculty);

        if (_faculties.Contains(faculty))
            throw MegaFacultyException.FacultyAlreadyExists(this, faculty);

        _faculties.Add(faculty);
        return faculty;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as MegaFaculty);
    }

    public bool Equals(MegaFaculty? other)
    {
        return other is not null && Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}