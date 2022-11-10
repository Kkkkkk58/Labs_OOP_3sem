using Backups.Models.Abstractions;
using Backups.Models.Repository.Abstractions;
using Backups.Tools.Algorithms.Abstractions;
using Backups.Tools.Archiver.Abstractions;
using Backups.Tools.Clock.Abstractions;

namespace Backups.Services.Configuration.Abstractions;

public interface IBackupTaskConfiguration
{
    IRepository TargetRepository { get; }
    IStorageAlgorithm StorageAlgorithm { get; }
    IArchiver Archiver { get; }
    IClock Clock { get; }
    string DateTimeFormat { get; }

    IRestorePointBuilder RestorePointBuilder { get; }
}