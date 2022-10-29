namespace Backups.Models.Abstractions;

public interface IRepository
{
    IRepositoryAccessKey BaseKey { get; }
    bool Contains(IRepositoryAccessKey accessKey);
    IReadOnlyList<RepositoryObject> GetData(IRepositoryAccessKey accessKey);
    Stream OpenStream(IRepositoryAccessKey accessKey);
}