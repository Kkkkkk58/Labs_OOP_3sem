namespace Backups.Models.Abstractions;

public interface IRepository
{
    IRepositoryAccessKey BaseKey { get; }
    bool Contains(IRepositoryAccessKey accessKey);
    IReadOnlyList<IRepositoryObject> GetData(IRepositoryAccessKey accessKey);
    Stream OpenStream(IRepositoryAccessKey accessKey);
}