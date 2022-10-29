using Backups.Models.Abstractions;

namespace Backups.Models;

public class LeafBackupObject : IBackupObject
{
    public LeafBackupObject(IRepository sourceRepository, RepositoryAccessKey accessKey)
    {
        ArgumentNullException.ThrowIfNull(sourceRepository);
        ArgumentNullException.ThrowIfNull(accessKey);

        AccessKey = accessKey;
        SourceRepository = sourceRepository;
    }

    public RepositoryAccessKey AccessKey { get; }
    public IRepository SourceRepository { get; }

    public IReadOnlyList<BackupObjectContent> GetContents()
    {
        var backupObjectContent = new BackupObjectContent(AccessKey, SourceRepository.GetData(AccessKey));
        return new List<BackupObjectContent> { backupObjectContent };
    }
}