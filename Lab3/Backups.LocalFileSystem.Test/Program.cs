using Backups.Entities;
using Backups.LocalFileSystem.Test;
using Backups.Models;
using Backups.Models.Abstractions;
using Backups.Models.Algorithms;

IRepository repository = new FileSystemRepository("D:/test");
Console.WriteLine("Hello, World!");

var bo = new BackupObject(repository, new FileSystemRepositoryAccessKey("1.txt"));
var bo2 = new BackupObject(repository, new FileSystemRepositoryAccessKey("2.txt"));
var backupTask = new BackupTask(new BackupConfiguration(new SplitStorageAlgorithm(), repository, new ZipArchiver(), new SimpleClock()), "Sample backup");
backupTask.TrackBackupObject(bo);
backupTask.TrackBackupObject(bo2);

RestorePoint restorePoint = backupTask.CreateRestorePoint();

backupTask.UntrackBackupObject(bo);
backupTask.UntrackBackupObject(bo2);

var bo3 = new BackupObject(repository, new FileSystemRepositoryAccessKey("aa"));

backupTask.TrackBackupObject(bo3);

restorePoint = backupTask.CreateRestorePoint();
backupTask.UntrackBackupObject(bo3);