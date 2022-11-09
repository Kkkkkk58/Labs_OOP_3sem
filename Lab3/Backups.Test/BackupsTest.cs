using Backups.Models;
using Backups.Models.Abstractions;
using Backups.Test.Repository;
using Backups.Tools.Algorithms;
using Backups.Tools.Algorithms.Abstractions;
using Xunit;
using Zio;
using Zio.FileSystems;

namespace Backups.Test;

public class BackupsTest : IDisposable
{
    private readonly MemoryFileSystem _fs;

    public BackupsTest()
    {
        _fs = new MemoryFileSystem();
        SeedFileSystem();
    }

    [Fact]
    public void SplitAlgorithmUntrackObject_StorageNumberNotEqualsRestorePointsNumber()
    {
        IRepository repository = new InMemoryRepository(_fs, "/test");
        var bo = new BackupObject(repository, new RepositoryAccessKey("input/test.txt", "/"));
        var bo2 = new BackupObject(repository, new RepositoryAccessKey("/input/dir", "/"));
        var algorithm = new SplitStorageAlgorithm();
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
        IRepository repository = new InMemoryRepository(_fs, "/test");

        var bo = new BackupObject(repository, new RepositoryAccessKey("input/test.txt", "/"));
        var bo2 = new BackupObject(repository, new RepositoryAccessKey("/input/dir", "/"));
        var algorithm = new SingleStorageAlgorithm();
        IBackupTask backupTask = GetBackupTask(repository, algorithm);
        backupTask.TrackBackupObject(bo);
        backupTask.TrackBackupObject(bo2);

        backupTask.CreateRestorePoint();

        backupTask.UntrackBackupObject(bo2);
        backupTask.CreateRestorePoint();

        Assert.Equal(2, GetRestorePointsNumber(backupTask));
        Assert.Equal(2, GetStorageNumber(backupTask));
    }

    public void Dispose()
    {
        _fs.Dispose();
    }

    private static int GetRestorePointsNumber(IBackupTask backupTask)
    {
        return backupTask
            .Backup
            .RestorePoints
            .Count;
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

    private int GetStorageNumber(IBackupTask backupTask)
    {
        return backupTask
            .Backup
            .RestorePoints
            .Select(bt => bt.Storage)
            .Select(storage => _fs.GetDirectoryEntry(new RepositoryAccessKey(storage.AccessKey.KeyParts.Take(storage.AccessKey.KeyParts.Count() - 1), "/").FullKey))
            .Select(de => de.EnumerateFiles().Count())
            .Sum();
    }

    private void SeedFileSystem()
    {
        using Stream data = GetData();
        _fs.CreateDirectory("/test/input");
        WriteData(data, _fs.CreateFile("/test/input/test.txt"));

        _fs.CreateDirectory("/test/input/dir");
        WriteData(data, _fs.CreateFile("/test/input/dir/suren.txt"));
    }
}