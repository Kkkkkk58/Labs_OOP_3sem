using Backups.Models.Storage.Abstractions;

namespace Backups.Models.Abstractions;

public interface IRestorePoint
{
    Guid Id { get; }
    DateTime CreationDate { get; }
    IStorage Storage { get; }
    IEnumerable<IBackupObject> BackupObjects { get; }
}