using Isu.Extra.Entities;

namespace Isu.Extra.Models;

public class ExtraStream : IStudyStream, IEquatable<ExtraStream>
{
    private readonly List<StudentDecorator> _students;
    private readonly int? _streamLimit;

    public ExtraStream(string name, int? streamLimit, ExtraCourse course, Schedule schedule)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new NotImplementedException();

        Id = Guid.NewGuid();
        Name = name;
        _streamLimit = streamLimit;
        _students = new List<StudentDecorator>();
        Course = course;
        Course.AddStream(this);
        Schedule = schedule;
    }

    public Guid Id { get; }
    public string Name { get; }
    public IReadOnlyList<StudentDecorator> Students => _students.AsReadOnly();
    public ExtraCourse Course { get; }
    public Schedule Schedule { get; }

    public StudentDecorator AddStudent(StudentDecorator student)
    {
        if (_students.Contains(student))
            throw new NotImplementedException();

        if (_streamLimit.HasValue && _students.Count == _streamLimit)
            throw new NotImplementedException();

        _students.Add(student);
        return student;
    }

    public void RemoveStudent(StudentDecorator student)
    {
        if (!_students.Remove(student))
            throw new NotImplementedException();
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