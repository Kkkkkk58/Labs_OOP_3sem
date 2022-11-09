namespace Backups.Models.Abstractions;

public interface IRepositoryObject
{
    string Name { get; }
    void Accept(IRepositoryObjectVisitor repositoryObjectVisitor);
}