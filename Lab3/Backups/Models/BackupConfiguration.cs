using Backups.Models.Abstractions;
using Backups.Models.Algorithms;

namespace Backups.Models;

public record BackupConfiguration : IBackupConfiguration
{
    public BackupConfiguration(IStorageAlgorithm storageAlgorithm, IRepository targetRepository, IArchiver archiver)
    {
        ArgumentNullException.ThrowIfNull(storageAlgorithm);
        ArgumentNullException.ThrowIfNull(targetRepository);
        ArgumentNullException.ThrowIfNull(archiver);

        StorageAlgorithm = storageAlgorithm;
        TargetRepository = targetRepository;
        Archiver = archiver;
    }

    public IRepository TargetRepository { get; }
    public IStorageAlgorithm StorageAlgorithm { get; }
    public IArchiver Archiver { get; }
}