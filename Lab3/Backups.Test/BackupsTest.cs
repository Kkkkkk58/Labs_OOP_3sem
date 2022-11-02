using Backups.Models;
using Backups.Models.Abstractions;
using Backups.Test.Repository;
using Backups.Tools.Algorithms;
using Backups.Tools.Algorithms.Abstractions;
using Xunit;
using Zio;
using Zio.FileSystems;

namespace Backups.Test;

public class BackupsTest
{
    [Fact]
    public void SplitAlgorithmUntrackObject_StorageNumberNotEqualsRestorePointsNumber()
    {
        var fs = new MemoryFileSystem();
        SeedFileSystem(fs);

        IRepository repository = new InMemoryRepository(fs, "/test");
        var bo = new BackupObject(repository, new InMemoryRepositoryAccessKey("input/test.txt"));
        var bo2 = new BackupObject(repository, new InMemoryRepositoryAccessKey("/input/dir"));
        var algorithm = new SplitStorageAlgorithm(Storage.Builder, ObjectStorageRelation.Builder);
        IBackupTask backupTask = GetBackupTask(repository, algorithm);

        backupTask.TrackBackupObject(bo);
        backupTask.TrackBackupObject(bo2);

        backupTask.CreateRestorePoint();

        backupTask.UntrackBackupObject(bo2);
        backupTask.CreateRestorePoint();

        Assert.Equal(2, GetRestorePointsNumber(backupTask));
        Assert.Equal(3, GetStorageNumber(backupTask));
    }

    [Fact]
    public void SingleAlgorithmUntrackObject_StorageNumberNotEqualsRestorePointsNumber()
    {
        var fs = new MemoryFileSystem();
        SeedFileSystem(fs);

        IRepository repository = new InMemoryRepository(fs, "/test");

        var bo = new BackupObject(repository, new InMemoryRepositoryAccessKey("input/test.txt"));
        var bo2 = new BackupObject(repository, new InMemoryRepositoryAccessKey("/input/dir"));
        var algorithm = new SingleStorageAlgorithm(Storage.Builder, ObjectStorageRelation.Builder);
        IBackupTask backupTask = GetBackupTask(repository, algorithm);
        backupTask.TrackBackupObject(bo);
        backupTask.TrackBackupObject(bo2);

        backupTask.CreateRestorePoint();

        backupTask.UntrackBackupObject(bo2);
        backupTask.CreateRestorePoint();

        Assert.Equal(2, GetRestorePointsNumber(backupTask));
        Assert.Equal(2, GetStorageNumber(backupTask));
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
            .SelectMany(bt => bt.ObjectStorageRelations.Select(r => r.Storage))
            .Distinct()
            .Count();
    }

    private static IBackupTask GetBackupTask(IRepository repository, IStorageAlgorithm algorithm)
    {
        IBackupTaskConfiguration config = GetBackupTaskConfig(repository, algorithm);

        return new BackupTaskBuilder()
            .SetConfiguration(config)
            .SetBackupName("Test Backup")
            .Build();
    }

    private static IBackupTaskConfiguration GetBackupTaskConfig(IRepository repository, IStorageAlgorithm algorithm)
    {
        return new BackupTaskConfigurationBuilder()
            .SetTargetRepository(repository)
            .SetStorageAlgorithm(algorithm)
            .Build();
    }

    private static void SeedFileSystem(IFileSystem fs)
    {
        using Stream data = GetData();
        fs.CreateDirectory("/test/input");
        WriteData(data, fs.CreateFile("/test/input/test.txt"));

        fs.CreateDirectory("/test/input/dir");
        WriteData(data, fs.CreateFile("/test/input/dir/suren.txt"));
    }

    private static Stream GetData()
    {
        const int size = 2000;
        byte[] data = Enumerable
            .Range(0, size)
            .Select(x => (byte)x)
            .ToArray();

        return new MemoryStream(data);
    }

    private static void WriteData(Stream inputStream, Stream outputStream)
    {
        using (outputStream)
        {
            inputStream.Seek(0, SeekOrigin.Begin);
            inputStream.CopyTo(outputStream);
        }
    }
}