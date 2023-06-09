﻿using Backups.Entities.Abstractions;
using Backups.Exceptions;
using Backups.Models.Abstractions;
using Backups.Models.Storage.Abstractions;
using Backups.Services.BackupTaskService.Abstractions;
using Backups.Services.BackupTaskService.Configuration.Abstractions;

namespace Backups.Services.BackupTaskService;

public class BackupTask : IBackupTask
{
    private readonly IBackup _backup;
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
        _backup = backup;
    }

    public IReadOnlyCollection<IRestorePoint> RestorePoints => _backup.RestorePoints;

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
        return _backup.AddRestorePoint(restorePoint);
    }

    public IBackupObject TrackBackupObject(IBackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupObject);
        if (_trackedObjects.Contains(backupObject))
            throw BackupTaskException.ObjectIsAlreadyTracked(backupObject);

        _trackedObjects.Add(backupObject);
        return backupObject;
    }

    public void UntrackBackupObject(IBackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupObject);

        if (!_trackedObjects.Remove(backupObject))
            throw BackupTaskException.ObjectNotFound(backupObject);
    }

    private bool DatePrecedesOtherRestorePointDate(DateTime restorePointDate)
    {
        return _backup.RestorePoints.Any(rp => rp.CreationDate >= restorePointDate);
    }

    private IRepositoryAccessKey GetRestorePointKey(DateTime restorePointDate)
    {
        return _config
            .TargetRepository
            .BaseKey
            .Combine(_backup.Id.ToString())
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