using Backups.Models;
using Backups.Models.Abstractions;

namespace Backups.Entities;

public class BackupTask
{
    private readonly List<IBackupObject> _trackedObjects;
    private readonly BackupConfiguration _backupConfiguration;
    private int _currentVersion;

    public BackupTask(BackupConfiguration backupConfiguration, string backupName)
        : this(new List<IBackupObject>(), backupConfiguration, default, new Backup(backupName))
    {
    }

    public BackupTask(List<IBackupObject> trackedObjects, BackupConfiguration backupConfiguration, int currentVersion, Backup backup)
    {
        ArgumentNullException.ThrowIfNull(trackedObjects);
        ArgumentNullException.ThrowIfNull(backup);
        if (currentVersion < 0)
            throw new ArgumentOutOfRangeException(nameof(currentVersion));

        _trackedObjects = trackedObjects;
        _backupConfiguration = backupConfiguration;
        _currentVersion = currentVersion;
        Backup = backup;
    }

    public Backup Backup { get; }

    public RestorePoint CreateRestorePoint(DateTime restorePointDate)
    {
        if (Backup.RestorePoints.Any(rp => rp.CreationDate >= restorePointDate))
            throw new NotImplementedException();

        IRepositoryAccessKey key = _backupConfiguration.TargetRepository.BaseKey
            .CombineWithSeparator(Backup.Id.ToString()).CombineWithSeparator(_currentVersion.ToString());

        IReadOnlyList<ObjectStorageRelation> relations =
            _backupConfiguration.StorageAlgorithm
                .CreateStorage(_trackedObjects, _backupConfiguration.TargetRepository, _backupConfiguration.Archiver, key)
                .ToList();

        var restorePoint = new RestorePoint(restorePointDate, ++_currentVersion, relations);
        return Backup.AddRestorePoint(restorePoint);
    }

    public IBackupObject TrackBackupObject(IBackupObject backupObject)
    {
        if (_trackedObjects.Contains(backupObject))
            throw new NotImplementedException();
        _trackedObjects.Add(backupObject);

        return backupObject;
    }

    public void UntrackBackupObject(IBackupObject backupObject)
    {
        if (!_trackedObjects.Remove(backupObject))
            throw new NotImplementedException();
    }
}