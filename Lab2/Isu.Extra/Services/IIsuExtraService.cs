using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Services;

namespace Isu.Extra.Services;

public interface IIsuExtraService : IIsuService, IExtraCoursesService
{
    IReadOnlyList<MegaFaculty> MegaFaculties { get; }
    IReadOnlyList<Faculty> Faculties { get; }

    MegaFaculty AddMegaFaculty(MegaFaculty megaFaculty);
    void InitGroupSchedule(Group group, Schedule schedule);
}