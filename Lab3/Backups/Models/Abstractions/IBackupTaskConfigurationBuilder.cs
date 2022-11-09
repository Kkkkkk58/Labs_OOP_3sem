using Backups.Tools.Algorithms.Abstractions;
using Backups.Tools.Archiver.Abstractions;
using Backups.Tools.Clock.Abstractions;

namespace Backups.Models.Abstractions;

public interface IBackupTaskConfigurationBuilder
{
    IBackupTaskAlgorithmBuilder SetTargetRepository(IRepository repository);
    IBackupTaskConfigurationBuilder SetArchiver(IArchiver archiver);
    IBackupTaskConfigurationBuilder SetClock(IClock clock);
    IBackupTaskConfigurationBuilder SetRestorePointBuilder(IRestorePointBuilder restorePointBuilder);
    IBackupTaskConfigurationBuilder SetDateTimeFormat(string dateTimeFormat);

    IBackupTaskConfiguration Build();
}

public interface IBackupTaskAlgorithmBuilder
{
    IBackupTaskConfigurationBuilder SetStorageAlgorithm(IStorageAlgorithm storageAlgorithm);
}