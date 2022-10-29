using Backups.Entities;
using Backups.LocalFileSystem.Test;
using Backups.Models;
using Backups.Models.Abstractions;
using Backups.Models.Algorithms;

IRepository repository = new FileSystemRepository(new DirectoryInfo("D:/test"));
Console.WriteLine("Hello, World!");

var bo = new LeafBackupObject(repository, new RepositoryAccessKey("1.txt"));
var bo2 = new LeafBackupObject(repository, new RepositoryAccessKey("2.txt"));
var backupTask = new BackupTask(new BackupConfiguration(new SingleStorageAlgorithm(), repository, new ZipArchiver()), "Sample backup");
backupTask.TrackBackupObject(bo);
backupTask.TrackBackupObject(bo2);

RestorePoint _ = backupTask.CreateRestorePoint(DateTime.Now);

backupTask.UntrackBackupObject(bo);
backupTask.UntrackBackupObject(bo2);

var bo3 = new CompositeBackupObject(repository, new RepositoryAccessKey("aa"));
var bo4 = new LeafBackupObject(repository, new RepositoryAccessKey("aa/58.txt"));
bo3.AddBackupObject(bo4);

backupTask.TrackBackupObject(bo3);

_ = backupTask.CreateRestorePoint(DateTime.Now);
backupTask.UntrackBackupObject(bo3);

var bo5 = new DirectoryBackupObject(repository, new RepositoryAccessKey("D:/test/aa"));
backupTask.TrackBackupObject(bo5);

_ = backupTask.CreateRestorePoint(DateTime.Now);