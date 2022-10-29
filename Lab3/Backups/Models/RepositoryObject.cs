namespace Backups.Models;

public record RepositoryObject
{
    public RepositoryObject(IRepositoryAccessKey accessKey, Stream stream)
    {
        AccessKey = accessKey;
        Stream = stream;
    }

    public IRepositoryAccessKey AccessKey { get; }
    public Stream Stream { get; }
}