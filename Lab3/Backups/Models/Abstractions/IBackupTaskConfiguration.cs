using Backups.Tools.Algorithms.Abstractions;
using Backups.Tools.Archivers.Abstractions;
using Backups.Tools.Clock.Abstractions;

namespace Backups.Models.Abstractions;

public interface IBackupTaskConfiguration
{
    IRepository TargetRepository { get; }
    IStorageAlgorithm StorageAlgorithm { get; }
    IArchiver Archiver { get; }
    IClock Clock { get; }

    IRestorePointBuilder RestorePointBuilder { get; }
}