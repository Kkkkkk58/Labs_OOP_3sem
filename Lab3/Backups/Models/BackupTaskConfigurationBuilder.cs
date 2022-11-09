using Backups.Models.Abstractions;
using Backups.Tools.Algorithms.Abstractions;
using Backups.Tools.Archiver;
using Backups.Tools.Archiver.Abstractions;
using Backups.Tools.Clock;
using Backups.Tools.Clock.Abstractions;

namespace Backups.Models;

public class BackupTaskConfigurationBuilder : IBackupTaskConfigurationBuilder, IBackupTaskAlgorithmBuilder
{
    private IRepository? _targetRepository;
    private IStorageAlgorithm? _storageAlgorithm;
    private IArchiver? _archiver;
    private IClock? _clock;
    private string? _dateTimeFormat;
    private IRestorePointBuilder? _restorePointBuilder;

    public IBackupTaskAlgorithmBuilder SetTargetRepository(IRepository repository)
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

    public IBackupTaskConfigurationBuilder SetDateTimeFormat(string dateTimeFormat)
    {
        _dateTimeFormat = dateTimeFormat;
        return this;
    }

    public IBackupTaskConfiguration Build()
    {
        ArgumentNullException.ThrowIfNull(_targetRepository);
        ArgumentNullException.ThrowIfNull(_storageAlgorithm);

        _archiver ??= new ZipArchiver();
        _dateTimeFormat ??= "yyyy-dd-MM_HH-mm-ss-fff";
        _restorePointBuilder ??= RestorePoint.Builder;
        _clock ??= new SimpleClock();

        return new BackupTaskConfiguration(
            _storageAlgorithm,
            _targetRepository,
            _archiver,
            _clock,
            _dateTimeFormat,
            _restorePointBuilder);
    }
}