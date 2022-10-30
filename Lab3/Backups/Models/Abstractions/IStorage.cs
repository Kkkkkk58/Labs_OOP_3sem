namespace Backups.Models.Abstractions;

public interface IStorage
{
    IRepositoryAccessKey AccessKey { get; }
    IRepository Repository { get; }
    IReadOnlyList<IRepositoryAccessKey> BackupObjectKeys { get; }
}