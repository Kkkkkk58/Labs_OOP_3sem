using Backups.Models.Abstractions;

namespace Backups.Models.RepositoryObjects.Abstractions;

public interface IFileRepositoryObject : IRepositoryObject
{
    Stream Stream { get; }
}