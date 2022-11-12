namespace Backups.Exceptions;

public class ArchivedFolderException : BackupsException
{
    private ArchivedFolderException(string message)
        : base(message)
    {
    }

    public static ArchivedFolderException ChildEntryNotFound(string objectName)
    {
        return new ArchivedFolderException($"An entry for object {objectName} was not found");
    }
}