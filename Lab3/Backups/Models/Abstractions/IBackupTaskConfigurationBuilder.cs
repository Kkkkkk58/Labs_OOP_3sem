using Backups.Models.Algorithms;

namespace Backups.Models.Abstractions;

public interface IBackupTaskConfigurationBuilder
{
    IBackupTaskConfigurationBuilder SetTargetRepository(IRepository repository);
    IBackupTaskConfigurationBuilder SetStorageAlgorithm(IStorageAlgorithm storageAlgorithm);
    IBackupTaskConfigurationBuilder SetArchiver(IArchiver archiver);
    IBackupTaskConfigurationBuilder SetClock(IClock clock);
    IBackupTaskConfigurationBuilder SetRestorePointBuilder(IRestorePointBuilder restorePointBuilder);

    IBackupTaskConfiguration Build();
}