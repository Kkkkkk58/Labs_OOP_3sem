using System.Text;
using Backups.Models;
using Backups.Models.Abstractions;
using Backups.Models.Algorithms;
using Backups.Test.Repository;
using Xunit;
using Zio;
using Zio.FileSystems;

namespace Backups.Test;

public class BackupsTest
{
    [Fact]
    public void BasicTest()
    {
        var fs = new MemoryFileSystem();
        SeedFileSystem(fs);

        IRepository repository = new InMemoryRepository(fs, "/test");

        var bo = new BackupObject(repository, new InMemoryRepositoryAccessKey("input/test.txt"));
        var bo2 = new BackupObject(repository, new InMemoryRepositoryAccessKey("/input/dir"));
        IBackupTask backupTask = GetBackupTask(repository);
        backupTask.TrackBackupObject(bo);
        backupTask.TrackBackupObject(bo2);

        backupTask.CreateRestorePoint();

        backupTask.UntrackBackupObject(bo2);
        backupTask.CreateRestorePoint();

        Assert.Equal(2, GetRestorePointsNumber(backupTask));
        Assert.Equal(3, GetStorageNumber(backupTask));
    }

    private static int GetRestorePointsNumber(IBackupTask backupTask)
    {
        return backupTask
            .Backup
            .RestorePoints
            .Count;
    }

    private static int GetStorageNumber(IBackupTask backupTask)
    {
        return backupTask
            .Backup
            .RestorePoints
            .Select(bt => bt.ObjectStorageRelations.Count)
            .Sum();
    }

    private static IBackupTask GetBackupTask(IRepository repository)
    {
        IBackupTaskConfiguration config = GetBackupTaskConfig(repository);

        return new BackupTaskBuilder()
            .SetConfiguration(config)
            .SetBackupName("Test Backup")
            .Build();
    }

    private static IBackupTaskConfiguration GetBackupTaskConfig(IRepository repository)
    {
        var algorithm = new SplitStorageAlgorithm(Storage.Builder, ObjectStorageRelation.Builder);

        return new BackupTaskConfigurationBuilder()
            .SetTargetRepository(repository)
            .SetStorageAlgorithm(algorithm)
            .Build();
    }

    private static void SeedFileSystem(IFileSystem fs)
    {
        fs.CreateDirectory("/test/input");
        using Stream stream = fs.CreateFile("/test/input/test.txt");
        {
            using Stream another = File.OpenRead("D:/test/1.txt");
            another.CopyTo(stream);
        }

        fs.CreateDirectory("/test/input/dir");
        using Stream anotherStream = fs.CreateFile("/test/input/dir/suren.txt");
        {
            using var ms = new MemoryStream(Encoding.UTF8.GetBytes("TEST MESSAGE"));
            ms.CopyTo(anotherStream);
        }
    }
}