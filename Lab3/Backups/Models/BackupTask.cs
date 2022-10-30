using Backups.Entities;
using Backups.Entities.Abstractions;
using Backups.Exceptions;
using Backups.Models.Abstractions;

namespace Backups.Models;

public class BackupTask
{
    private readonly List<IBackupObject> _trackedObjects;
    private readonly IBackupTaskConfiguration _config;
    private IRestorePointVersion _currentVersion;

    // TODO BackupTaskBuilderImpl -> default values
    public BackupTask(IBackupTaskConfiguration config, string backupName)
        : this(new List<IBackupObject>(), config, new RestorePointVersion(), new Backup(backupName))
    {
    }

    public BackupTask(
        List<IBackupObject> trackedObjects,
        IBackupTaskConfiguration config,
        IRestorePointVersion currentVersion,
        IBackup backup)
    {
        ArgumentNullException.ThrowIfNull(trackedObjects);
        ArgumentNullException.ThrowIfNull(config);
        ArgumentNullException.ThrowIfNull(currentVersion);
        ArgumentNullException.ThrowIfNull(backup);

        _trackedObjects = trackedObjects;
        _config = config;
        _currentVersion = currentVersion;
        Backup = backup;
    }

    public IBackup Backup { get; }

    public IRestorePoint CreateRestorePoint()
    {
        DateTime restorePointDate = _config.Clock.Now;
        if (Backup.RestorePoints.Any(rp => rp.CreationDate >= restorePointDate))
            throw BackupTaskException.InvalidRestorePointCreationDate(restorePointDate);

        _currentVersion = _currentVersion.GetNext();
        IRepositoryAccessKey restorePointKey = GetRestorePointKey();

        IReadOnlyList<IObjectStorageRelation> relations = _config
                .StorageAlgorithm
                .CreateStorage(_trackedObjects, _config.TargetRepository, _config.Archiver, restorePointKey)
                .ToList();

        var restorePoint = new RestorePoint(restorePointDate, _currentVersion, relations);
        return Backup.AddRestorePoint(restorePoint);
    }

    public IBackupObject TrackBackupObject(IBackupObject backupObject)
    {
        if (_trackedObjects.Contains(backupObject))
            throw BackupTaskException.ObjectIsAlreadyTracked(backupObject);
        _trackedObjects.Add(backupObject);

        return backupObject;
    }

    public void UntrackBackupObject(IBackupObject backupObject)
    {
        if (!_trackedObjects.Remove(backupObject))
            throw BackupTaskException.ObjectNotFound(backupObject);
    }

    private IRepositoryAccessKey GetRestorePointKey()
    {
        return _config
            .TargetRepository
            .BaseKey
            .Combine(Backup.Id.ToString())
            .Combine(_currentVersion.ToString());
    }
}