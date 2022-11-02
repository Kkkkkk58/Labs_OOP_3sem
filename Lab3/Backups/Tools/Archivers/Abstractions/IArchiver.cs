using Backups.Models.Abstractions;

namespace Backups.Tools.Archivers.Abstractions;

public interface IArchiver
{
    string Extension { get; }
    void Archive(IEnumerable<IBackupObject> backupObjects, Stream writingStream);
}