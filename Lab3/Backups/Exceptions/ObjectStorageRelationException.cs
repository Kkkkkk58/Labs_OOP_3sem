namespace Backups.Exceptions;

public class ObjectStorageRelationException : BackupsException
{
    private ObjectStorageRelationException(string message)
        : base(message)
    {
    }

    public static ObjectStorageRelationException InvalidRelation()
    {
        return new ObjectStorageRelationException("Entities can't make an object-storage connection");
    }
}