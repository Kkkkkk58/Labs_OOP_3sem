using Backups.Models.Abstractions;

namespace Backups.Models;

public class BackupObject : IBackupObject
{
    public BackupObject(IRepository sourceRepository, IRepositoryAccessKey accessKey)
    {
        ArgumentNullException.ThrowIfNull(sourceRepository);
        ArgumentNullException.ThrowIfNull(accessKey);

        AccessKey = accessKey;
        SourceRepository = sourceRepository;
    }

    public IRepositoryAccessKey AccessKey { get; }
    public IRepository SourceRepository { get; }

    public IReadOnlyList<IRepositoryObject> GetContents()
    {
        return SourceRepository.GetData(AccessKey);
    }

    public override string ToString()
    {
        return $"Backup object: {SourceRepository} - {AccessKey}";
    }
}