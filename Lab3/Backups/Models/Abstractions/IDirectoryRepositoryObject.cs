namespace Backups.Models.Abstractions;

public interface IDirectoryRepositoryObject : IRepositoryObject
{
    IReadOnlyCollection<IRepositoryObject> Children { get; }
}