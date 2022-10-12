namespace Isu.Extra.Models;

public class MegaFaculty : IEquatable<MegaFaculty>
{
    private readonly List<Faculty> _faculties;

    public MegaFaculty(string name)
        : this(name, new List<Faculty>())
    {
    }

    public MegaFaculty(string name, List<Faculty> faculties)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new NotImplementedException();
        if (faculties.Distinct().Count() < faculties.Count)
            throw new NotImplementedException();

        Id = Guid.NewGuid();
        Name = name;
        _faculties = faculties;
    }

    public Guid Id { get; }
    public string Name { get; }
    public IReadOnlyList<Faculty> Faculties => _faculties;

    public Faculty AddFaculty(Faculty faculty)
    {
        if (_faculties.Contains(faculty))
            throw new NotImplementedException();

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