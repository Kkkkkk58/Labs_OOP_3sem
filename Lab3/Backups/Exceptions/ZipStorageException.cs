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

    public static ZipStorageException InvalidRepositoryObject()
    {
        throw new ZipStorageException("Zip storage received an invalid object from repository");
    }
}