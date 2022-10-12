using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Models;
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

    public IReadOnlyList<MegaFaculty> MegaFaculties => _megaFaculties.AsReadOnly();
    public IReadOnlyList<ExtraCourse> ExtraCourses => _extraCourses.AsReadOnly();

    public MegaFaculty AddMegaFaculty(MegaFaculty megaFaculty)
    {
        if (_megaFaculties.Contains(megaFaculty))
            throw new NotImplementedException();
        _megaFaculties.Add(megaFaculty);
        return megaFaculty;
    }

    public ExtraCourse AddExtraCourse(ExtraCourse extraCourse)
    {
        if (_extraCourses.Contains(extraCourse))
            throw new NotImplementedException();
        _extraCourses.Add(extraCourse);
        return extraCourse;
    }

    public void SignUpStudentForExtraStream(Student student, ExtraStream extraStream)
    {
        StudentDecorator studentDecorator = GetStudentDecorator(student);
        studentDecorator.SignUpToExtraStream(extraStream);
    }

    public void ResetStudentExtraStream(Student student, ExtraStream extraStream)
    {
        StudentDecorator studentDecorator = GetStudentDecorator(student);
        studentDecorator.ResetExtraStream(extraStream);
    }

    public void InitGroupSchedule(Group group, Schedule schedule)
    {
        if (_groupDecorators.ContainsKey(group))
            throw new NotImplementedException();
        if (_isuService.FindGroup(group.Name) is null)
            throw new NotImplementedException();

        var groupDecorator = new GroupDecorator(group, schedule);
        _groupDecorators.Add(group, groupDecorator);
    }

    public IReadOnlyList<StudentDecorator> GetUnassignedStudents(Group group)
    {
        return group
            .Students
            .Select(GetStudentDecorator)
            .Where(studentDecorator => !studentDecorator.IsAssignedToAllStreams)
            .ToList()
            .AsReadOnly();
    }

    private StudentDecorator GetStudentDecorator(Student decoratee)
    {
        if (_studentDecorators.TryGetValue(decoratee, out StudentDecorator? studentDecorator))
            return studentDecorator;

        if (_isuService.FindStudent(decoratee.PersonalInfo.Id.Value) is null)
            throw new NotImplementedException();

        GroupDecorator groupDecorator = GetGroupDecorator(decoratee.Group);
        studentDecorator = new StudentDecorator(decoratee, groupDecorator, _options.ExtraStreamsLimit);
        _studentDecorators.Add(decoratee, studentDecorator);
        return studentDecorator;
    }

    private GroupDecorator GetGroupDecorator(Group decoratee)
    {
        if (_groupDecorators.TryGetValue(decoratee, out GroupDecorator? groupDecorator))
            return groupDecorator;
        throw new NotImplementedException();
    }
}