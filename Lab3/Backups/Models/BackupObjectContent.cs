namespace Backups.Models;

public record BackupObjectContent
{
    public BackupObjectContent(RepositoryAccessKey accessKey, Stream stream)
    {
        AccessKey = accessKey;
        Stream = stream;
    }

    public RepositoryAccessKey AccessKey { get; }
    public Stream Stream { get; }
}