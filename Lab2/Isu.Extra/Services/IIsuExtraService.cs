using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Services;

namespace Isu.Extra.Services;

public interface IIsuExtraService : IIsuService, IExtraCoursesService
{
    IReadOnlyCollection<MegaFaculty> MegaFaculties { get; }
    IReadOnlyCollection<Faculty> Faculties { get; }
    IReadOnlyCollection<StudentDecorator> Students { get; }
    IReadOnlyCollection<GroupDecorator> Groups { get; }

    StudentDecorator GetStudentDecorator(Student student);
    GroupDecorator GetGroupDecorator(Group group);
    MegaFaculty AddMegaFaculty(MegaFaculty megaFaculty);
    void InitGroupSchedule(Group group, Schedule schedule);
}