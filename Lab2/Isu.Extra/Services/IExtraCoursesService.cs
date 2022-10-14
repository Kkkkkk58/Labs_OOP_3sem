using Isu.Entities;
using Isu.Extra.Entities;

namespace Isu.Extra.Services;

public interface IExtraCoursesService
{
    IReadOnlyCollection<ExtraCourse> ExtraCourses { get; }
    IReadOnlyCollection<ExtraStream> ExtraStreams { get; }

    ExtraCourse AddExtraCourse(ExtraCourse extraCourse);
    void SignUpStudentForExtraStream(Student student, ExtraStream extraStream);
    void ResetStudentExtraStream(Student student, ExtraStream extraStream);
    IReadOnlyCollection<StudentDecorator> GetUnassignedStudents(Group group);
}