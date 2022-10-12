using Isu.Entities;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class StudentDecorator : IEquatable<StudentDecorator>
{
    private readonly List<ExtraStream> _extraStreams;
    private readonly GroupDecorator _groupDecorator;
    private readonly int _extraStreamsLimit;

    public StudentDecorator(Student studentDecoratee, GroupDecorator groupDecorator, int extraStreamsLimit)
    {
        if (!studentDecoratee.Group.Equals(groupDecorator.GroupDecoratee))
            throw new NotImplementedException();
        if (_extraStreamsLimit < 0)
            throw new NotImplementedException();

        StudentDecoratee = studentDecoratee;
        _groupDecorator = groupDecorator;
        _extraStreams = new List<ExtraStream>();
        _extraStreamsLimit = extraStreamsLimit;
    }

    public Student StudentDecoratee { get; }
    public IReadOnlyList<ExtraStream> ExtraStreams => _extraStreams;
    public bool IsAssignedToAllStreams => _extraStreams.Count == _extraStreamsLimit;
    public Schedule Schedule => GetSchedule();

    public void SignUpToExtraStream(ExtraStream extraStream)
    {
        if (IsAssignedToAllStreams)
            throw new NotImplementedException();
        if (_extraStreams.Any(stream => stream.Course.Equals(extraStream.Course)))
            throw new NotImplementedException();
        if (extraStream.Course.Provider.Faculties.Any(faculty =>
                faculty.Letter.Equals(StudentDecoratee.Group.Name.Details.FacultyLetter)))
            throw new NotImplementedException();
        if (Schedule.HasIntersections(extraStream.Schedule))
            throw new NotImplementedException();

        extraStream.AddStudent(this);
        _extraStreams.Add(extraStream);
    }

    public void ResetExtraStream(ExtraStream extraStream)
    {
        extraStream.RemoveStudent(this);
        if (!_extraStreams.Remove(extraStream))
            throw new NotImplementedException();
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as StudentDecorator);
    }

    public bool Equals(StudentDecorator? other)
    {
        return other is not null && StudentDecoratee.Equals(other.StudentDecoratee);
    }

    public override int GetHashCode()
    {
        return StudentDecoratee.GetHashCode();
    }

    private Schedule GetSchedule()
    {
        Schedule schedule = _groupDecorator.Schedule;
        foreach (ExtraStream extraStream in ExtraStreams)
        {
            schedule.Combine(extraStream.Schedule);
        }

        return schedule;
    }
}