using Backups.LocalFileSystem.Test.Repository;
using Backups.Models;
using Backups.Models.Abstractions;
using Backups.Tools.Algorithms;

string separator = Path.DirectorySeparatorChar.ToString();
IRepository repository = new FileSystemRepository("D:/test");
Console.WriteLine("Hello, World!");

var bo = new BackupObject(repository, new RepositoryAccessKey("1.txt", separator));
var bo2 = new BackupObject(repository, new RepositoryAccessKey("2.txt", separator));
IBackupTask backupTask = GetBackupTask(repository);

backupTask.TrackBackupObject(bo);
backupTask.TrackBackupObject(bo2);

backupTask.CreateRestorePoint();

backupTask.UntrackBackupObject(bo);

var bo3 = new BackupObject(repository, new RepositoryAccessKey("aa", separator));

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
    var algorithm = new SplitStorageAlgorithm();

    return new BackupTaskConfigurationBuilder()
        .SetTargetRepository(repository)
        .SetStorageAlgorithm(algorithm)
        .Build();
}