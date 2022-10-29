using Backups.Models.Abstractions;
using Backups.Models.Algorithms;

namespace Backups.Models;

public record BackupConfiguration : IBackupConfiguration
{
    public BackupConfiguration(IStorageAlgorithm storageAlgorithm, IRepository targetRepository, IArchiver archiver, IClock clock)
    {
        ArgumentNullException.ThrowIfNull(storageAlgorithm);
        ArgumentNullException.ThrowIfNull(targetRepository);
        ArgumentNullException.ThrowIfNull(archiver);
        ArgumentNullException.ThrowIfNull(clock);

        StorageAlgorithm = storageAlgorithm;
        TargetRepository = targetRepository;
        Archiver = archiver;
        Clock = clock;
    }

    public IRepository TargetRepository { get; }
    public IStorageAlgorithm StorageAlgorithm { get; }
    public IArchiver Archiver { get; }
    public IClock Clock { get; }
}