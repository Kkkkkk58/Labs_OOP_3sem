using Backups.Algorithms.Abstractions;
using Backups.Archivers.Abstractions;
using Backups.Models.Abstractions;
using Backups.Tools.Clock.Abstractions;

namespace Backups.Models;

public record BackupTaskConfiguration : IBackupTaskConfiguration
{
    public BackupTaskConfiguration(
        IStorageAlgorithm storageAlgorithm,
        IRepository targetRepository,
        IArchiver archiver,
        IClock clock,
        IRestorePointBuilder restorePointBuilder)
    {
        ValidateArguments(storageAlgorithm, targetRepository, archiver, clock, restorePointBuilder);

        StorageAlgorithm = storageAlgorithm;
        TargetRepository = targetRepository;
        Archiver = archiver;
        Clock = clock;
        RestorePointBuilder = restorePointBuilder;
    }

    public IRepository TargetRepository { get; }
    public IStorageAlgorithm StorageAlgorithm { get; }
    public IArchiver Archiver { get; }
    public IClock Clock { get; }

    public IRestorePointBuilder RestorePointBuilder { get; }

    private static void ValidateArguments(
        IStorageAlgorithm storageAlgorithm,
        IRepository targetRepository,
        IArchiver archiver,
        IClock clock,
        IRestorePointBuilder restorePointBuilder)
    {
        ArgumentNullException.ThrowIfNull(storageAlgorithm);
        ArgumentNullException.ThrowIfNull(targetRepository);
        ArgumentNullException.ThrowIfNull(archiver);
        ArgumentNullException.ThrowIfNull(clock);
        ArgumentNullException.ThrowIfNull(restorePointBuilder);
    }
}