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
            fs.CreateDirectory("/input");
            using Stream stream = fs.CreateFile("/input/test.txt");
            {
                using Stream another = File.OpenRead("D:/test/1.txt");
                another.CopyTo(stream);
            }
        }

        IRepository repository = new InMemoryRepository(fs);

        var bo = new LeafBackupObject(repository, new RepositoryAccessKey("/input/test.txt"));
        var backupTask = new BackupTask(new BackupConfiguration(new SplitStorageAlgorithm(), repository, new ZipArchiver()), "Sample backup");
        backupTask.TrackBackupObject(bo);

        backupTask.CreateRestorePoint(DateTime.Now);
        Assert.True(true);
    }
}