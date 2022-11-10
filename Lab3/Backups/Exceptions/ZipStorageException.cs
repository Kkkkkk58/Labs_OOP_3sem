namespace Backups.Exceptions;

public class ZipStorageException : BackupsException
{
    private ZipStorageException(string message)
        : base(message)
    {
    }

    public static ZipStorageException ZipEntryNotFound(string objectName)
    {
        return new ZipStorageException($"An entry for object {objectName} was not found");
    }
}