using Backups.Models;
using Backups.Models.Abstractions;

namespace Backups.Entities;

public class BackupTask
{
    private readonly List<Models.Abstractions.IBackupObject> _trackedObjects;
    private readonly BackupConfiguration _backupConfiguration;
    private readonly Backup _backup;
    private int _currentVersion;

    public BackupTask(BackupConfiguration backupConfiguration, string backupName)
        : this(new List<Models.Abstractions.IBackupObject>(), backupConfiguration, default, new Backup(backupName))
    {
    }

    public BackupTask(List<Models.Abstractions.IBackupObject> trackedObjects, BackupConfiguration backupConfiguration, int currentVersion, Backup backup)
    {
        ArgumentNullException.ThrowIfNull(trackedObjects);
        ArgumentNullException.ThrowIfNull(backup);
        if (currentVersion < 0)
            throw new ArgumentOutOfRangeException(nameof(currentVersion));

        _trackedObjects = trackedObjects;
        _backupConfiguration = backupConfiguration;
        _currentVersion = currentVersion;
        _backup = backup;
    }

    public RestorePoint CreateRestorePoint(DateTime restorePointDate)
    {
        if (_backup.RestorePoints.Any(rp => rp.CreationDate >= restorePointDate))
            throw new NotImplementedException();

        IReadOnlyList<ObjectStorageRelation> relations =
            _backupConfiguration.StorageAlgorithm
                .CreateStorage(_trackedObjects, _backupConfiguration.TargetRepository, _backupConfiguration.Archiver)
                .ToList();

        var restorePoint = new RestorePoint(restorePointDate, ++_currentVersion, relations);
        return _backup.AddRestorePoint(restorePoint);
    }

    public IBackupObject TrackBackupObject(Models.Abstractions.IBackupObject backupObject)
    {
        if (_trackedObjects.Contains(backupObject))
            throw new NotImplementedException();
        _trackedObjects.Add(backupObject);

        return backupObject;
    }

    public void UntrackBackupObject(Models.Abstractions.IBackupObject backupObject)
    {
        if (!_trackedObjects.Remove(backupObject))
            throw new NotImplementedException();
    }
}