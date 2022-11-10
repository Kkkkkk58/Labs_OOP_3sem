using Backups.Models.Abstractions;
using Backups.Models.RepositoryObjects.Abstractions;

namespace Backups.Models.Repository.Abstractions;

public interface IRepository
{
    IRepositoryAccessKey BaseKey { get; }
    bool Contains(IRepositoryAccessKey accessKey);
    IRepositoryObject GetComponent(IRepositoryAccessKey accessKey);
    Stream OpenWrite(IRepositoryAccessKey accessKey);
}