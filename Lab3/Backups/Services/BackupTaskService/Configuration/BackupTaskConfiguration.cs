using Backups.Models.Abstractions;
using Backups.Models.Repository.Abstractions;
using Backups.Services.Configuration.Abstractions;
using Backups.Tools.Algorithms.Abstractions;
using Backups.Tools.Archiver.Abstractions;
using Backups.Tools.Clock.Abstractions;

namespace Backups.Services.Configuration;

public record BackupTaskConfiguration : IBackupTaskConfiguration
{
    public BackupTaskConfiguration(
        IStorageAlgorithm storageAlgorithm,
        IRepository targetRepository,
        IArchiver archiver,
        IClock clock,
        string dateTimeFormat,
        IRestorePointBuilder restorePointBuilder)
    {
        ArgumentNullException.ThrowIfNull(storageAlgorithm);
        ArgumentNullException.ThrowIfNull(targetRepository);
        ArgumentNullException.ThrowIfNull(archiver);
        ArgumentNullException.ThrowIfNull(clock);
        ArgumentNullException.ThrowIfNull(dateTimeFormat);
        ArgumentNullException.ThrowIfNull(restorePointBuilder);

        StorageAlgorithm = storageAlgorithm;
        TargetRepository = targetRepository;
        Archiver = archiver;
        Clock = clock;
        DateTimeFormat = dateTimeFormat;
        RestorePointBuilder = restorePointBuilder;
    }

    public IRepository TargetRepository { get; }
    public IStorageAlgorithm StorageAlgorithm { get; }
    public IArchiver Archiver { get; }
    public IClock Clock { get; }
    public string DateTimeFormat { get; }

    public IRestorePointBuilder RestorePointBuilder { get; }
}