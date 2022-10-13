using Isu.Entities;
using Isu.Extra.Entities;

namespace Isu.Extra.Services;

public interface IExtraCoursesService
{
    IReadOnlyList<ExtraCourse> ExtraCourses { get; }
    IReadOnlyList<ExtraStream> ExtraStreams { get; }

    ExtraCourse AddExtraCourse(ExtraCourse extraCourse);
    void SignUpStudentForExtraStream(Student student, ExtraStream extraStream);
    void ResetStudentExtraStream(Student student, ExtraStream extraStream);
    IReadOnlyList<StudentDecorator> GetUnassignedStudents(Group group);
}