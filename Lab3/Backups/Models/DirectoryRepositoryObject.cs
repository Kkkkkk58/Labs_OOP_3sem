using Backups.Models.Abstractions;

namespace Backups.Models;

public class DirectoryRepositoryObject : IDirectoryRepositoryObject
{
    private readonly Func<IReadOnlyCollection<IRepositoryObject>> _func;

    public DirectoryRepositoryObject(string name, Func<IReadOnlyCollection<IRepositoryObject>> func)
    {
        Name = name;
        _func = func;
    }

    public IReadOnlyCollection<IRepositoryObject> Children => _func.Invoke();

    public string Name { get; }

    public void Accept(IRepositoryObjectVisitor repositoryObjectVisitor)
    {
        repositoryObjectVisitor.Visit(this);
    }
}