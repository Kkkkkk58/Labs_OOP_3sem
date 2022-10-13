namespace Isu.Extra.Exceptions;

public class MegaFacultyException : IsuExtraException
{
    public MegaFacultyException(string message)
        : base(message)
    {
    }

    public static MegaFacultyException EmptyName()
    {
        throw new MegaFacultyException("The name of a megaFaculty can't be empty");
    }

    public static Exception FacultyAlreadyExists(Guid megaFacultyId, Guid facultyId)
    {
        throw new MegaFacultyException($"Faculty {facultyId} already exists in megaFaculty {megaFacultyId}");
    }
}