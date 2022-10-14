using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Models.IsuInformationDetails;
using Isu.Services;

namespace Isu.Extra.Services;

public class IsuExtraService : IIsuExtraService
{
    private readonly IsuExtraServiceOptions _options;
    private readonly IIsuService _isuService;
    private readonly List<MegaFaculty> _megaFaculties;
    private readonly Dictionary<Student, StudentDecorator> _studentDecorators;
    private readonly Dictionary<Group, GroupDecorator> _groupDecorators;
    private readonly List<ExtraCourse> _extraCourses;

    public IsuExtraService(IsuExtraServiceOptions options, IIsuService isuService)
    {
        _options = options;
        _isuService = isuService;
        _megaFaculties = new List<MegaFaculty>();
        _studentDecorators = new Dictionary<Student, StudentDecorator>();
        _groupDecorators = new Dictionary<Group, GroupDecorator>();
        _extraCourses = new List<ExtraCourse>();
    }

    public IReadOnlyCollection<StudentDecorator> Students => _studentDecorators.Values;
    public IReadOnlyCollection<GroupDecorator> Groups => _groupDecorators.Values;
    public IReadOnlyCollection<MegaFaculty> MegaFaculties => _megaFaculties.AsReadOnly();

    public IReadOnlyCollection<Faculty> Faculties =>
        MegaFaculties.SelectMany(megaFaculty => megaFaculty.Faculties).ToList().AsReadOnly();

    public IReadOnlyCollection<ExtraCourse> ExtraCourses => _extraCourses.AsReadOnly();

    public IReadOnlyCollection<ExtraStream> ExtraStreams =>
        ExtraCourses.SelectMany(course => course.Streams).ToList().AsReadOnly();

    public MegaFaculty AddMegaFaculty(MegaFaculty megaFaculty)
    {
        ArgumentNullException.ThrowIfNull(megaFaculty);
        if (_megaFaculties.Contains(megaFaculty))
            throw IsuExtraServiceException.MegaFacultyAlreadyExists(megaFaculty);
        if (Faculties.Any(faculty => megaFaculty.Faculties.Contains(faculty)))
            throw IsuExtraServiceException.MegaFacultyContainsAlienFaculty(megaFaculty);

        _megaFaculties.Add(megaFaculty);
        return megaFaculty;
    }

    public ExtraCourse AddExtraCourse(ExtraCourse extraCourse)
    {
        ArgumentNullException.ThrowIfNull(extraCourse);
        if (_extraCourses.Contains(extraCourse))
            throw IsuExtraServiceException.ExtraCourseAlreadyExists(extraCourse);
        if (!_megaFaculties.Contains(extraCourse.Provider))
            throw IsuExtraServiceException.ExtraCourseProviderNotFound(extraCourse, extraCourse.Provider);

        _extraCourses.Add(extraCourse);
        return extraCourse;
    }

    public void SignUpStudentForExtraStream(Student student, ExtraStream extraStream)
    {
        ArgumentNullException.ThrowIfNull(student);
        ArgumentNullException.ThrowIfNull(extraStream);

        StudentDecorator studentDecorator = GetStudentDecorator(student);
        studentDecorator.SignUpToExtraStream(extraStream);
    }

    public void ResetStudentExtraStream(Student student, ExtraStream extraStream)
    {
        ArgumentNullException.ThrowIfNull(student);
        ArgumentNullException.ThrowIfNull(extraStream);

        StudentDecorator studentDecorator = GetStudentDecorator(student);
        studentDecorator.ResetExtraStream(extraStream);
    }

    public void InitGroupSchedule(Group group, Schedule schedule)
    {
        ArgumentNullException.ThrowIfNull(group);
        ArgumentNullException.ThrowIfNull(schedule);

        if (_groupDecorators.ContainsKey(group))
            throw IsuExtraServiceException.GroupAlreadyHasSchedule(group);
        if (_isuService.FindGroup(group.Name) is null)
            throw IsuExtraServiceException.GroupNotFound(group);

        var groupDecorator = new GroupDecorator(group, schedule);
        _groupDecorators.Add(group, groupDecorator);
    }

    public IReadOnlyCollection<Student> GetUnassignedStudents(Group group)
    {
        ArgumentNullException.ThrowIfNull(group);

        if (!_groupDecorators.ContainsKey(group))
            return group.Students;

        return group
            .Students
            .Where(student => !GetStudentDecorator(student).IsAssignedToAllStreams)
            .ToList()
            .AsReadOnly();
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        ArgumentNullException.ThrowIfNull(student);
        ArgumentNullException.ThrowIfNull(newGroup);

        StudentDecorator studentDecorator = GetStudentDecorator(student);
        GroupDecorator newGroupDecorator = GetGroupDecorator(newGroup);

        studentDecorator.ChangeDecoratedGroup(newGroupDecorator, newGroup);
    }

    public StudentDecorator GetStudentDecorator(Student student)
    {
        ArgumentNullException.ThrowIfNull(student);

        if (_studentDecorators.TryGetValue(student, out StudentDecorator? studentDecorator))
            return studentDecorator;

        if (_isuService.FindStudent(student.PersonalInfo.Id.Value) is null)
            throw IsuExtraServiceException.StudentNotFound(student);

        GroupDecorator groupDecorator = GetGroupDecorator(student.Group);
        studentDecorator = new StudentDecorator(student, groupDecorator, _options.StudentExtraCoursesLimit);
        _studentDecorators.Add(student, studentDecorator);
        return studentDecorator;
    }

    public GroupDecorator GetGroupDecorator(Group group)
    {
        ArgumentNullException.ThrowIfNull(group);

        if (_groupDecorators.TryGetValue(group, out GroupDecorator? groupDecorator))
            return groupDecorator;
        throw IsuExtraServiceException.GroupScheduleIsNotSet(group);
    }

    public Group AddGroup(GroupName name)
    {
        return _isuService.AddGroup(name);
    }

    public Student AddStudent(Group group, string name)
    {
        return _isuService.AddStudent(group, name);
    }

    public Student GetStudent(int id)
    {
        return _isuService.GetStudent(id);
    }

    public Student? FindStudent(int id)
    {
        return _isuService.FindStudent(id);
    }

    public IReadOnlyList<Student> FindStudents(GroupName groupName)
    {
        return _isuService.FindStudents(groupName);
    }

    public IReadOnlyList<Student> FindStudents(CourseNumber courseNumber)
    {
        return _isuService.FindStudents(courseNumber);
    }

    public Group? FindGroup(GroupName groupName)
    {
        return _isuService.FindGroup(groupName);
    }

    public IReadOnlyList<Group> FindGroups(CourseNumber courseNumber)
    {
        return _isuService.FindGroups(courseNumber);
    }
}