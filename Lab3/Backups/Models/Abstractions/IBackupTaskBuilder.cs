﻿using Backups.Entities.Abstractions;

namespace Backups.Models.Abstractions;

public interface IBackupTaskBuilder
{
    IBackupTaskBuilder SetConfiguration(IBackupTaskConfiguration configuration);
    IBackupTaskBuilder SetBackup(IBackup backup);
    IBackupTaskBuilder SetBackupName(string backupName);
    IBackupTaskBuilder SetTrackedObjects(IEnumerable<IBackupObject> trackedObjects);

    IBackupTask Build();
}