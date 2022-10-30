using Backups.Models.Algorithms;

namespace Backups.Models.Abstractions;

public interface IBackupTaskConfiguration
{
    IRepository TargetRepository { get; }
    IStorageAlgorithm StorageAlgorithm { get; }
    IArchiver Archiver { get; }
    IClock Clock { get; }
}