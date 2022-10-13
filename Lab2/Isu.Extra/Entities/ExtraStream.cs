using Isu.Extra.Exceptions;

namespace Isu.Extra.Entities;

public class ExtraStream : IEquatable<ExtraStream>
{
    private readonly List<StudentDecorator> _students;
    private readonly int? _streamLimit;

    public ExtraStream(string name, int? streamLimit, ExtraCourse course, Schedule schedule)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentOutOfRangeException(nameof(name));

        Id = Guid.NewGuid();
        Name = name;
        _streamLimit = streamLimit;
        _students = new List<StudentDecorator>();
        Course = course ?? throw new ArgumentNullException(nameof(course));
        Schedule = schedule ?? throw new ArgumentNullException(nameof(schedule));
        Course.AddStream(this);
    }

    public Guid Id { get; }
    public string Name { get; }
    public IReadOnlyList<StudentDecorator> Students => _students.AsReadOnly();
    public ExtraCourse Course { get; }
    public Schedule Schedule { get; }

    public StudentDecorator AddStudent(StudentDecorator student)
    {
        ArgumentNullException.ThrowIfNull(student);

        if (_students.Contains(student))
            throw ExtraStreamException.StudentAlreadyExists(this, student.PersonalInfo.Id);
        if (_streamLimit.HasValue && _students.Count == _streamLimit)
            throw ExtraStreamException.StreamLimitReached(this, _streamLimit.Value);

        _students.Add(student);
        return student;
    }

    public void RemoveStudent(StudentDecorator student)
    {
        ArgumentNullException.ThrowIfNull(student);

        if (!_students.Remove(student))
            throw ExtraStreamException.StudentNotFound(this, student.PersonalInfo.Id);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as ExtraStream);
    }

    public bool Equals(ExtraStream? other)
    {
        return other is not null && Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}