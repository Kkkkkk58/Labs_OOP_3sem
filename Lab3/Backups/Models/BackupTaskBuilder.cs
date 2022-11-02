using Backups.Entities;
using Backups.Entities.Abstractions;
using Backups.Models.Abstractions;

namespace Backups.Models;

public class BackupTaskBuilder : IBackupTaskBuilder
{
    private IBackupTaskConfiguration? _config;
    private IRestorePointVersion? _restorePointVersion;
    private IBackup? _backup;
    private string? _backupName;
    private IEnumerable<IBackupObject>? _trackedObjects;

    public IBackupTaskBuilder SetConfiguration(IBackupTaskConfiguration configuration)
    {
        _config = configuration;
        return this;
    }

    public IBackupTaskBuilder SetCurrentVersion(IRestorePointVersion restorePointVersion)
    {
        _restorePointVersion = restorePointVersion;
        return this;
    }

    public IBackupTaskBuilder SetBackup(IBackup backup)
    {
        _backup = backup;
        return this;
    }

    public IBackupTaskBuilder SetBackupName(string backupName)
    {
        _backupName = backupName;
        return this;
    }

    public IBackupTaskBuilder SetTrackedObjects(IEnumerable<IBackupObject> trackedObjects)
    {
        _trackedObjects = trackedObjects;
        return this;
    }

    public IBackupTask Build()
    {
        ArgumentNullException.ThrowIfNull(_config);
        _restorePointVersion ??= new RestorePointVersion();
        _backup ??= GetNewBackup();
        _trackedObjects ??= new List<IBackupObject>();

        return new BackupTask(_config, _restorePointVersion, _backup, _trackedObjects);
    }

    private IBackup GetNewBackup()
    {
        return (_backupName is null)
            ? throw new ArgumentNullException(nameof(_backupName))
            : new Backup(_backupName);
    }
}