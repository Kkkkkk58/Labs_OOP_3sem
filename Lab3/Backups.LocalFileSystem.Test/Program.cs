using Backups.LocalFileSystem.Test;
using Backups.Models;
using Backups.Models.Abstractions;
using Backups.Models.Algorithms;

IRepository repository = new FileSystemRepository("D:/test");
Console.WriteLine("Hello, World!");

var bo = new BackupObject(repository, new FileSystemRepositoryAccessKey("1.txt"));
var bo2 = new BackupObject(repository, new FileSystemRepositoryAccessKey("2.txt"));
IBackupTask backupTask = GetBackupTask(repository);

backupTask.TrackBackupObject(bo);
backupTask.TrackBackupObject(bo2);

backupTask.CreateRestorePoint();

backupTask.UntrackBackupObject(bo);
backupTask.UntrackBackupObject(bo2);

var bo3 = new BackupObject(repository, new FileSystemRepositoryAccessKey("aa"));

backupTask.TrackBackupObject(bo3);

backupTask.CreateRestorePoint();
backupTask.UntrackBackupObject(bo3);

static IBackupTask GetBackupTask(IRepository repository)
{
    IBackupTaskConfiguration backupTaskConfig = GetBackupTaskConfig(repository);

    IBackupTask backupTask = new BackupTaskBuilder()
        .SetConfiguration(backupTaskConfig)
        .SetBackupName("Sample backup")
        .Build();

    return backupTask;
}

static IBackupTaskConfiguration GetBackupTaskConfig(IRepository repository)
{
    var algorithm = new SplitStorageAlgorithm(new StorageBuilder(), new ObjectStorageRelationBuilder());

    return new BackupTaskConfigurationBuilder()
        .SetStorageAlgorithm(algorithm)
        .SetTargetRepository(repository)
        .Build();
}