using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Models;

namespace Isu.Extra.Services;

public interface IIsuExtraService
{
    IReadOnlyList<MegaFaculty> MegaFaculties { get; }
    IReadOnlyList<ExtraCourse> ExtraCourses { get; }
    MegaFaculty AddMegaFaculty(MegaFaculty megaFaculty);
    ExtraCourse AddExtraCourse(ExtraCourse extraCourse);
    void SignUpStudentForExtraStream(Student student, ExtraStream extraStream);
    void ResetStudentExtraStream(Student student, ExtraStream extraStream);
    void InitGroupSchedule(Group group, Schedule schedule);
    IReadOnlyList<StudentDecorator> GetUnassignedStudents(Group group);
}