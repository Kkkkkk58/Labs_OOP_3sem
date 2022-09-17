using Isu.Models.IsuInformationDetails;

namespace Isu.Exceptions;

public class GroupException : IsuException
{
    private GroupException(string message)
        : base(message)
    {
    }

    public static GroupException GroupAlreadyExists(GroupName groupName)
    {
        return new GroupException($"The group with name {groupName} already exists");
    }

    public static GroupException GroupLimitReached(GroupName groupName)
    {
        return new GroupException($"The group {groupName} has already reached max number of students");
    }

    public static GroupException GroupNotFound(GroupName groupName)
    {
        return new GroupException($"The group with name {groupName} was not found");
    }

    public static GroupException StudentNotFoundInGroup(IsuId studentId, GroupName groupName)
    {
        return new GroupException($"The student with id={studentId} was not found in group {groupName}");
    }

    public static GroupException StudentAlreadyInGroup(IsuId studentId, GroupName groupName)
    {
        return new GroupException($"The student with id={studentId} is already in group {groupName}");
    }
}