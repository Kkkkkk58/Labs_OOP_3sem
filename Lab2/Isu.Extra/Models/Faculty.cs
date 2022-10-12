using Isu.Models.IsuInformationDetails;

namespace Isu.Extra.Models;

public class Faculty : IEquatable<Faculty>
{
    public Faculty(string name, FacultyLetter letter)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new NotImplementedException();

        Id = Guid.NewGuid();
        Name = name;
        Letter = letter;
    }

    public Guid Id { get; }
    public string Name { get; }
    public FacultyLetter Letter { get; }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Faculty);
    }

    public bool Equals(Faculty? other)
    {
        return other is not null && Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}