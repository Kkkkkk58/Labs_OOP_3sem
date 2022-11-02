using Backups.Models.Abstractions;

namespace Backups.Archivers.Abstractions;

public interface IArchiver
{
    string Extension { get; }
    void Archive(IEnumerable<IBackupObject> backupObjects, Stream writingStream);
}