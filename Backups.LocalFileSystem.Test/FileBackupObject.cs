using Backups.Models;
using Backups.Models.Abstractions;

namespace Backups.LocalFileSystem.Test;

public class FileBackupObject : IFileSystemBackupObject
{
    public FileBackupObject(IRepository sourceRepository, RepositoryAccessKey accessKey)
    {
        AccessKey = accessKey;
        SourceRepository = sourceRepository;
    }

    public RepositoryAccessKey AccessKey { get; }
    public IRepository SourceRepository { get; }

    public IReadOnlyList<BackupObjectContent> GetContents()
    {
        var backupObjectContent = new BackupObjectContent(AccessKey, File.OpenRead(AccessKey.Value));
        return new List<BackupObjectContent> { backupObjectContent };
    }
}