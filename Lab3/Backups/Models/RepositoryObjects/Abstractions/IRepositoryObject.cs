using Backups.Models.Visitors.Abstractions;

namespace Backups.Models.RepositoryObjects.Abstractions;

public interface IRepositoryObject
{
    string Name { get; }
    void Accept(IRepositoryObjectVisitor repositoryObjectVisitor);
}