using Backups.Models.Abstractions;

namespace Backups.Models;

public record RepositoryObject : IRepositoryObject
{
    public RepositoryObject(IRepositoryAccessKey accessKey, Stream stream)
    {
        ArgumentNullException.ThrowIfNull(accessKey);
        ArgumentNullException.ThrowIfNull(stream);

        AccessKey = accessKey;
        Stream = stream;
    }

    public IRepositoryAccessKey AccessKey { get; }
    public Stream Stream { get; }
}