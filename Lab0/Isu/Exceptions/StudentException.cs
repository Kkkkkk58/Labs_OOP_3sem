using Isu.Models.IsuInformationDetails;

namespace Isu.Exceptions;

public class StudentException : IsuException
{
    private StudentException(string message)
        : base(message)
    {
    }

    public static StudentException StudentNotFound(int studentId)
    {
        return new StudentException($"The student with id={studentId} was not found");
    }

    public static StudentException SameGroupTransfer(IsuId studentId, GroupName groupName)
    {
        return new StudentException($"Unable to move student with id={studentId} to the same group {groupName}");
    }
}