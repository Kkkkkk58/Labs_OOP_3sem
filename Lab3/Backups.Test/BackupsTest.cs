using System.Text;
using Backups.Entities;
using Backups.Models;
using Backups.Models.Abstractions;
using Backups.Models.Algorithms;
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

        IRepository repository = new InMemoryRepository(fs, "/test");

        var bo = new BackupObject(repository, new InMemoryRepositoryAccessKey("input/test.txt"));
        var bo2 = new BackupObject(repository, new InMemoryRepositoryAccessKey("/input/dir"));
        var backupTask = new BackupTask(new BackupConfiguration(new SplitStorageAlgorithm(), repository, new ZipArchiver()), "Sample backup");
        backupTask.TrackBackupObject(bo);
        backupTask.TrackBackupObject(bo2);

        backupTask.CreateRestorePoint(DateTime.Now);

        backupTask.UntrackBackupObject(bo2);
        backupTask.CreateRestorePoint(DateTime.Now);

        Assert.Equal(2, backupTask.Backup.RestorePoints.Count);
        Assert.Equal(3, backupTask.Backup.RestorePoints.Select(bt => bt.ObjectStorageRelations.Count).Sum());
    }
}