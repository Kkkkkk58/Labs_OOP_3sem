using Isu.Extra.Entities;

namespace Isu.Extra.Exceptions;

public class MegaFacultyException : IsuExtraException
{
    public MegaFacultyException(string message)
        : base(message)
    {
    }

    public static Exception FacultyAlreadyExists(MegaFaculty megaFaculty, Faculty faculty)
    {
        throw new MegaFacultyException($"Faculty {faculty.Id} already exists in megaFaculty {megaFaculty.Id}");
    }
}