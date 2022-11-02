using Backups.Algorithms.Abstractions;
using Backups.Archivers.Abstractions;
using Backups.Tools.Clock.Abstractions;

namespace Backups.Models.Abstractions;

public interface IBackupTaskConfigurationBuilder
{
    IBackupTaskAlgorithmBuilder SetTargetRepository(IRepository repository);
    IBackupTaskConfigurationBuilder SetArchiver(IArchiver archiver);
    IBackupTaskConfigurationBuilder SetClock(IClock clock);
    IBackupTaskConfigurationBuilder SetRestorePointBuilder(IRestorePointBuilder restorePointBuilder);

    IBackupTaskConfiguration Build();
}

public interface IBackupTaskAlgorithmBuilder
{
    IBackupTaskConfigurationBuilder SetStorageAlgorithm(IStorageAlgorithm storageAlgorithm);
}