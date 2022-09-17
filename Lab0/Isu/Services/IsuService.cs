using Isu.Entities;
using Isu.Exceptions;
using Isu.Models.Abstractions;
using Isu.Models.IsuInformationDetails;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private readonly int? _groupLimit;
    private readonly IIsuIdGenerator _idGenerator;
    private readonly List<Student> _students;
    private readonly List<Group> _groups;

    public IsuService(IsuServiceOptions options, IIsuIdGenerator idGenerator)
        : this(options, idGenerator, new List<Student>(), new List<Group>())
    {
    }

    public IsuService(
        IsuServiceOptions options,
        IIsuIdGenerator idGenerator,
        List<Student> students,
        List<Group> groups)
    {
        _groupLimit = options.GroupLimit;
        _idGenerator = idGenerator;
        _students = students;
        _groups = groups;
    }

    public IReadOnlyList<Student> Students => _students;
    public IReadOnlyList<Group> Groups => _groups;

    public Group AddGroup(GroupName name)
    {
        if (GroupWithNameExists(name))
            throw GroupException.GroupAlreadyExists(name);

        var group = new Group(name, _groupLimit);
        _groups.Add(group);

        return group;
    }

    public Student AddStudent(Group group, string name)
    {
        if (GroupDoesNotExist(group))
            throw GroupException.GroupNotFound(group.Name);

        var personalInfo = new PersonalInformation(_idGenerator.GetNext(), name);
        var student = new Student(personalInfo, group);

        _students.Add(student);

        return student;
    }

    public Student GetStudent(int id)
    {
        Student? resultFoundById = FindStudent(id);

        return resultFoundById ?? throw StudentException.StudentNotFound(id);
    }

    public Student? FindStudent(int id)
    {
        return _students.Find(student => student.PersonalInfo.Id.Value == id);
    }

    public IReadOnlyList<Student> FindStudents(GroupName groupName)
    {
        return _groups
            .Single(group => group.Name.Equals(groupName))
            .Students;
    }

    public IReadOnlyList<Student> FindStudents(CourseNumber courseNumber)
    {
        return _students
            .Where(student => student.Group.Name.Details.CourseNumber.Equals(courseNumber))
            .ToList();
    }

    public Group? FindGroup(GroupName groupName)
    {
        return _groups.Find(group => group.Name.Equals(groupName));
    }

    public IReadOnlyList<Group> FindGroups(CourseNumber courseNumber)
    {
        return _groups
            .Where(group => group.Name.Details.CourseNumber.Equals(courseNumber))
            .ToList();
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        if (StudentDoesNotExist(student))
            throw StudentException.StudentNotFound(student.PersonalInfo.Id.Value);

        Group oldGroup = student.Group;
        if (GroupDoesNotExist(oldGroup))
            throw GroupException.GroupNotFound(oldGroup.Name);

        if (GroupDoesNotExist(newGroup))
            throw GroupException.GroupNotFound(newGroup.Name);

        student.ChangeGroup(newGroup);
    }

    private bool GroupWithNameExists(GroupName name)
    {
        return _groups.Any(group => group.Name.Equals(name));
    }

    private bool GroupDoesNotExist(Group group)
    {
        return !_groups.Contains(group);
    }

    private bool StudentDoesNotExist(Student student)
    {
        return !_students.Contains(student);
    }
}