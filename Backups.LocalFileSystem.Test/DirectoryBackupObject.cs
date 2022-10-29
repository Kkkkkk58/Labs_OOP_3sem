using Backups.Models;
using Backups.Models.Abstractions;

namespace Backups.LocalFileSystem.Test;

public class DirectoryBackupObject : IFileSystemBackupObject
{
    public DirectoryBackupObject(IRepository sourceRepository, RepositoryAccessKey accessKey)
    {
        AccessKey = accessKey;
        SourceRepository = sourceRepository;
    }

    public RepositoryAccessKey AccessKey { get; }
    public IRepository SourceRepository { get; }

    public IReadOnlyList<BackupObjectContent> GetContents()
    {
        var contents = new List<BackupObjectContent>();
        var directoryInfo = new DirectoryInfo(AccessKey.Value);
        string parentDir = directoryInfo.Parent?.FullName ?? "";
        foreach (FileInfo file in directoryInfo.EnumerateFiles())
        {
            string name = Path.GetRelativePath(parentDir, file.FullName);
            var content = new BackupObjectContent(new RepositoryAccessKey(name), File.OpenRead(file.FullName));
            contents.Add(content);
        }

        return contents;
    }
}