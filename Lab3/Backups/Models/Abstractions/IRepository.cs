namespace Backups.Models.Abstractions;

public interface IRepository
{
    IRepositoryAccessKey BaseKey { get; }
    bool Contains(IRepositoryAccessKey accessKey);
    IRepositoryObject GetComponent(IRepositoryAccessKey accessKey);
    Stream OpenWrite(IRepositoryAccessKey accessKey);
}