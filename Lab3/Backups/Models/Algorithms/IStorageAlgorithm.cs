using Backups.Models.Abstractions;

namespace Backups.Models.Algorithms;

public interface IStorageAlgorithm
{
    IEnumerable<ObjectStorageRelation> CreateStorage(IEnumerable<Abstractions.IBackupObject> backupObjects, IRepository targetRepository, IArchiver archiver);
}