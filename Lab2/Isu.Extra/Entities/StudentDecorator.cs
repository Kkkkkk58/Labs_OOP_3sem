using Isu.Entities;
using Isu.Extra.Exceptions;
using Isu.Models.IsuInformationDetails;

namespace Isu.Extra.Entities;

public class StudentDecorator : IEquatable<StudentDecorator>
{
    private readonly Student _student;
    private readonly List<ExtraStream> _extraStreams;
    private readonly int _extraStreamsLimit;
    private GroupDecorator _groupDecorator;

    public StudentDecorator(Student student, GroupDecorator groupDecorator, int extraStreamsLimit)
    {
        _student = student ?? throw new ArgumentNullException(nameof(student));
        _groupDecorator = groupDecorator ?? throw new ArgumentNullException(nameof(groupDecorator));

        if (!_groupDecorator.Name.Equals(_student.Group.Name))
        {
            throw StudentDecoratorException.InvalidGroupDecorator(
                _student.PersonalInfo.Id,
                _student.Group.Name,
                groupDecorator.Name);
        }

        if (extraStreamsLimit < 0)
            throw StudentDecoratorException.ExtraStreamsLimitOutOfRange(extraStreamsLimit);

        _extraStreamsLimit = extraStreamsLimit;
        _extraStreams = new List<ExtraStream>(_extraStreamsLimit);
    }

    public IReadOnlyList<ExtraStream> ExtraStreams => _extraStreams;
    public bool IsAssignedToAllStreams => _extraStreams.Count == _extraStreamsLimit;
    public Schedule Schedule => GetSchedule();
    public PersonalInformation PersonalInfo => _student.PersonalInfo;

    public void SignUpToExtraStream(ExtraStream extraStream)
    {
        if (extraStream is null)
            throw new ArgumentNullException(nameof(extraStream));
        ValidateSigningUpAbility(extraStream);

        extraStream.AddStudent(this);
        _extraStreams.Add(extraStream);
    }

    public void ResetExtraStream(ExtraStream extraStream)
    {
        if (extraStream is null)
            throw new ArgumentNullException(nameof(extraStream));

        extraStream.RemoveStudent(this);
        if (!_extraStreams.Remove(extraStream))
            throw new NotImplementedException();
    }

    public void ChangeDecoratedGroup(GroupDecorator newGroupDecorator, Group decoratedGroup)
    {
        if (!newGroupDecorator.Name.Equals(decoratedGroup.Name))
            throw new NotImplementedException();

        Schedule extraSchedule = GetExtraSchedule();
        if (extraSchedule.HasIntersections(newGroupDecorator.Schedule))
            throw new NotImplementedException();
        if (_extraStreams.Any(stream => IsExtraStreamProvidedByGroupMegaFaculty(stream, decoratedGroup)))
            throw new NotImplementedException();

        _student.ChangeGroup(decoratedGroup);
        _groupDecorator = newGroupDecorator;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as StudentDecorator);
    }

    public bool Equals(StudentDecorator? other)
    {
        return other is not null && _student.Equals(other._student);
    }

    public override int GetHashCode()
    {
        return _student.GetHashCode();
    }

    private static bool IsExtraStreamProvidedByGroupMegaFaculty(ExtraStream extraStream, Group curGroup)
    {
        return extraStream.Course.Provider.Faculties
            .Any(faculty => faculty.Letter.Equals(curGroup.Name.Details.FacultyLetter));
    }

    private void ValidateSigningUpAbility(ExtraStream extraStream)
    {
        if (IsAssignedToAllStreams)
            throw StudentDecoratorException.StreamsLimitReached(PersonalInfo.Id, _extraStreamsLimit);
        if (_extraStreams.Any(stream => stream.Course.Equals(extraStream.Course)))
        {
            throw StudentDecoratorException.AlreadySignedForCourse(
                PersonalInfo.Id,
                extraStream.Course.Id,
                extraStream.Id);
        }

        if (IsExtraStreamProvidedByGroupMegaFaculty(extraStream, _student.Group))
        {
            throw StudentDecoratorException.SameCourseMegaFaculty(
                PersonalInfo.Id,
                extraStream.Course.Provider.Id,
                extraStream.Id);
        }

        if (Schedule.HasIntersections(extraStream.Schedule))
            throw StudentDecoratorException.ScheduleIntersectsWithExtraStream(PersonalInfo.Id, extraStream.Id);
    }

    private Schedule GetSchedule()
    {
        Schedule mainSchedule = _groupDecorator.Schedule;
        Schedule extraSchedule = GetExtraSchedule();

        return mainSchedule.Combine(extraSchedule);
    }

    private Schedule GetExtraSchedule()
    {
        Schedule extraSchedule = Schedule.Builder.Build();

        foreach (ExtraStream extraStream in ExtraStreams)
        {
            extraSchedule.Combine(extraStream.Schedule);
        }

        return extraSchedule;
    }
}