using Backups.Models.Abstractions;

namespace Backups.Models;

public class BackupObject : IBackupObject
{
    public BackupObject(IRepository sourceRepository, IRepositoryAccessKey accessKey)
        : this(Guid.NewGuid(), sourceRepository, accessKey)
    {
    }

    public BackupObject(Guid id, IRepository sourceRepository, IRepositoryAccessKey accessKey)
    {
        ArgumentNullException.ThrowIfNull(sourceRepository);
        ArgumentNullException.ThrowIfNull(accessKey);

        Id = id;
        AccessKey = accessKey;
        SourceRepository = sourceRepository;
    }

    public Guid Id { get; }
    public IRepositoryAccessKey AccessKey { get; }
    public IRepository SourceRepository { get; }

    public IReadOnlyList<IRepositoryObject> GetContents()
    {
        return SourceRepository.GetData(AccessKey);
    }
}