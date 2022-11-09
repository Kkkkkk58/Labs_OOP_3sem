using Backups.Tools.Algorithms.Abstractions;
using Backups.Tools.Archiver.Abstractions;
using Backups.Tools.Clock.Abstractions;

namespace Backups.Models.Abstractions;

public interface IBackupTaskConfiguration
{
    IRepository TargetRepository { get; }
    IStorageAlgorithm StorageAlgorithm { get; }
    IArchiver Archiver { get; }
    IClock Clock { get; }
    string DateTimeFormat { get; }

    IRestorePointBuilder RestorePointBuilder { get; }
}