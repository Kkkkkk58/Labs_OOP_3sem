using Backups.Entities;
using Backups.LocalFileSystem.Test;
using Backups.Models;
using Backups.Models.Abstractions;
using Backups.Models.Algorithms;

IRepository repository = new FileSystemRepository("D:/test");
Console.WriteLine("Hello, World!");

var bo = new BackupObject(repository, new FileSystemRepositoryAccessKey("1.txt"));
var bo2 = new BackupObject(repository, new FileSystemRepositoryAccessKey("2.txt"));
var backupTask = new BackupTask(new BackupConfiguration(new SingleStorageAlgorithm(), repository, new ZipArchiver()), "Sample backup");
backupTask.TrackBackupObject(bo);
backupTask.TrackBackupObject(bo2);

RestorePoint restorePoint = backupTask.CreateRestorePoint(DateTime.Now);

backupTask.UntrackBackupObject(bo);
backupTask.UntrackBackupObject(bo2);

var bo3 = new BackupObject(repository, new FileSystemRepositoryAccessKey("aa"));

backupTask.TrackBackupObject(bo3);

restorePoint = backupTask.CreateRestorePoint(DateTime.Now);
backupTask.UntrackBackupObject(bo3);