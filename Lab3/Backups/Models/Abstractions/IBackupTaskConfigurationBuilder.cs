using Backups.Algorithms.Abstractions;
using Backups.Archivers.Abstractions;
using Backups.Tools.Clock.Abstractions;

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