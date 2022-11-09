using Backups.Entities.Abstractions;
using Backups.Exceptions;
using Backups.Models.Abstractions;
using Backups.Models.Storage.Abstractions;

namespace Backups.Models;

public class BackupTask : IBackupTask
{
    private readonly List<IBackupObject> _trackedObjects;
    private readonly IBackupTaskConfiguration _config;

    public BackupTask(
        IBackupTaskConfiguration config,
        IBackup backup,
        IEnumerable<IBackupObject> trackedObjects)
    {
        ArgumentNullException.ThrowIfNull(trackedObjects);
        ArgumentNullException.ThrowIfNull(config);
        ArgumentNullException.ThrowIfNull(backup);

        _trackedObjects = trackedObjects.ToList();
        _config = config;
        Backup = backup;
    }

    public IBackup Backup { get; }

    public IRestorePoint CreateRestorePoint()
    {
        DateTime restorePointDate = _config.Clock.Now;
        if (DatePrecedesOtherRestorePointDate(restorePointDate))
            throw BackupTaskException.InvalidRestorePointCreationDate(restorePointDate);

        IRepositoryAccessKey restorePointKey = GetRestorePointKey(restorePointDate);

        IStorage storage = _config
            .StorageAlgorithm
            .CreateStorage(_trackedObjects, _config.TargetRepository, _config.Archiver, restorePointKey);

        IRestorePoint restorePoint = GetRestorePoint(restorePointDate, storage);
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

    private bool DatePrecedesOtherRestorePointDate(DateTime restorePointDate)
    {
        return Backup.RestorePoints.Any(rp => rp.CreationDate >= restorePointDate);
    }

    private IRepositoryAccessKey GetRestorePointKey(DateTime restorePointDate)
    {
        return _config
            .TargetRepository
            .BaseKey
            .Combine(Backup.Id.ToString())
            .Combine(restorePointDate.ToString(_config.DateTimeFormat));
    }

    private IRestorePoint GetRestorePoint(
        DateTime restorePointDate,
        IStorage storage)
    {
        return _config
            .RestorePointBuilder
            .SetDate(restorePointDate)
            .SetBackupObjects(_trackedObjects.AsEnumerable())
            .SetStorage(storage)
            .Build();
    }
}