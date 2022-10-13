using Isu.Extra.Exceptions;
using Isu.Models.IsuInformationDetails;

namespace Isu.Extra.Entities;

public class Faculty : IEquatable<Faculty>
{
    public Faculty(string name, FacultyLetter letter)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw FacultyException.EmptyName();

        Id = Guid.NewGuid();
        Name = name;
        Letter = letter ?? throw new ArgumentNullException(nameof(letter));
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