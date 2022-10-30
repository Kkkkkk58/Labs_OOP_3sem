using Backups.Models.Abstractions;

namespace Backups.Models;

public class BackupObject : IBackupObject
{
    public BackupObject(IRepository sourceRepository, IRepositoryAccessKey accessKey)
    {
        AccessKey = accessKey;
        SourceRepository = sourceRepository;
    }

    public IRepositoryAccessKey AccessKey { get; }
    public IRepository SourceRepository { get; }

    public IReadOnlyList<RepositoryObject> GetContents()
    {
        return SourceRepository.GetData(AccessKey);
    }
}