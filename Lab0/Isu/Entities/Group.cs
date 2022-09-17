using Isu.Exceptions;
using Isu.Models.IsuInformationDetails;

namespace Isu.Entities;

public class Group : IEquatable<Group>
{
    private readonly List<Student> _students;
    private readonly int? _groupLimit;

    public Group(GroupName name, int? groupLimit = null)
    {
        Name = name;
        _students = new List<Student>();
        _groupLimit = groupLimit;
    }

    public GroupName Name { get; }
    public IReadOnlyList<Student> Students => _students;

    public void RemoveStudent(Student student)
    {
        if (!_students.Remove(student))
            throw GroupException.StudentNotFoundInGroup(student.PersonalInfo.Id, Name);
    }

    public Student AddStudent(Student student)
    {
        if (_students.Contains(student))
            throw GroupException.StudentAlreadyInGroup(student.PersonalInfo.Id, Name);

        if (IsGroupLimitReached())
            throw GroupException.GroupLimitReached(Name);

        _students.Add(student);
        return student;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Group);
    }

    public bool Equals(Group? other)
    {
        return other is not null
               && Name.Equals(other.Name);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    private bool IsGroupLimitReached()
    {
        return _groupLimit.HasValue
               && _students.Count == _groupLimit;
    }
}