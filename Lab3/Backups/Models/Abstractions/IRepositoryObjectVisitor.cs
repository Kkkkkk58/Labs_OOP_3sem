namespace Backups.Models.Abstractions;

public interface IRepositoryObjectVisitor
{
    void Visit(IFileRepositoryObject fileRepositoryObject);
    void Visit(IDirectoryRepositoryObject directoryRepositoryObject);

    IEnumerable<IArchivedObject> GetArchivedObjects();
}