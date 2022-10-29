namespace Backups.Models.Abstractions;

public interface IArchiver
{
    string Extension { get; }
    void Archive(IEnumerable<IBackupObject> backupObjects, Stream writingStream);
}