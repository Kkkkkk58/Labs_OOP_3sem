using Isu.Entities;
using Isu.Extra.Entities;

namespace Isu.Extra.Exceptions;

public class IsuExtraServiceException : IsuExtraException
{
    private IsuExtraServiceException(string message)
        : base(message)
    {
    }

    public static IsuExtraServiceException MegaFacultyAlreadyExists(MegaFaculty megaFaculty)
    {
        throw new IsuExtraServiceException($"MegaFaculty {megaFaculty.Id} already exists");
    }

    public static IsuExtraServiceException MegaFacultyContainsAlienFaculty(MegaFaculty megaFaculty)
    {
        throw new IsuExtraServiceException(
            $"MegaFaculty {megaFaculty} contains a faculty that belongs to other megaFaculty");
    }

    public static IsuExtraServiceException ExtraCourseAlreadyExists(ExtraCourse extraCourse)
    {
        throw new IsuExtraServiceException($"Extra course {extraCourse} already exists");
    }

    public static IsuExtraServiceException ExtraCourseProviderNotFound(
        ExtraCourse extraCourse,
        MegaFaculty extraCourseProvider)
    {
        throw new IsuExtraServiceException(
            $"Extra course {extraCourse.Id} provider megaFaculty {extraCourseProvider.Id} was not found");
    }

    public static IsuExtraServiceException GroupAlreadyHasSchedule(Group group)
    {
        throw new IsuExtraServiceException($"Group {group.Name} already has schedule");
    }

    public static IsuExtraServiceException GroupNotFound(Group group)
    {
        throw new IsuExtraServiceException($"Group {group.Name} was not found");
    }

    public static IsuExtraServiceException StudentNotFound(Student student)
    {
        throw new IsuExtraServiceException($"Student {student.PersonalInfo.Id} was not found");
    }

    public static IsuExtraServiceException GroupScheduleIsNotSet(Group group)
    {
        throw new IsuExtraServiceException($"Group {group.Name} schedule was not set");
    }
}