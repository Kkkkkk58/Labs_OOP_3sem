using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Services;

namespace Isu.Extra.Services;

public interface IIsuExtraService : IIsuService, IExtraCoursesService
{
    IReadOnlyList<MegaFaculty> MegaFaculties { get; }
    IReadOnlyList<Faculty> Faculties { get; }
    IReadOnlyList<StudentDecorator> Students { get; }
    IReadOnlyList<GroupDecorator> Groups { get; }

    StudentDecorator GetStudentDecorator(Student student);
    GroupDecorator GetGroupDecorator(Group group);
    MegaFaculty AddMegaFaculty(MegaFaculty megaFaculty);
    void InitGroupSchedule(Group group, Schedule schedule);
}