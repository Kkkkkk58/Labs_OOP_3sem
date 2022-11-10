namespace Backups.Models.RepositoryObjects.Abstractions;

public interface IDirectoryRepositoryObject : IRepositoryObject
{
    IReadOnlyCollection<IRepositoryObject> Children { get; }
}