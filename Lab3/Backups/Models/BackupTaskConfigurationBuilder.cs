using Backups.Algorithms.Abstractions;
using Backups.Archivers.Abstractions;
using Backups.Models.Abstractions;
using Backups.Models.Archivers;
using Backups.Tools.Clock;
using Backups.Tools.Clock.Abstractions;

namespace Backups.Models;

public class BackupTaskConfigurationBuilder : IBackupTaskConfigurationBuilder
{
    private IRepository? _targetRepository;
    private IStorageAlgorithm? _storageAlgorithm;
    private IArchiver? _archiver;
    private IClock? _clock;
    private IRestorePointBuilder? _restorePointBuilder;

    public IBackupTaskConfigurationBuilder SetTargetRepository(IRepository repository)
    {
        _targetRepository = repository;
        return this;
    }

    public IBackupTaskConfigurationBuilder SetStorageAlgorithm(IStorageAlgorithm storageAlgorithm)
    {
        _storageAlgorithm = storageAlgorithm;
        return this;
    }

    public IBackupTaskConfigurationBuilder SetArchiver(IArchiver archiver)
    {
        _archiver = archiver;
        return this;
    }

    public IBackupTaskConfigurationBuilder SetClock(IClock clock)
    {
        _clock = clock;
        return this;
    }

    public IBackupTaskConfigurationBuilder SetRestorePointBuilder(IRestorePointBuilder restorePointBuilder)
    {
        _restorePointBuilder = restorePointBuilder;
        return this;
    }

    public IBackupTaskConfiguration Build()
    {
        ArgumentNullException.ThrowIfNull(_targetRepository);
        ArgumentNullException.ThrowIfNull(_storageAlgorithm);

        _archiver ??= new ZipArchiver();
        _restorePointBuilder ??= new RestorePointBuilder();
        _clock ??= new SimpleClock();

        return new BackupTaskConfiguration(
            _storageAlgorithm,
            _targetRepository,
            _archiver,
            _clock,
            _restorePointBuilder);
    }
}