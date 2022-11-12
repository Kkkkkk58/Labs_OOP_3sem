using Backups.Models.RepositoryObjects.Abstractions;
using Backups.Models.Visitors.Abstractions;

namespace Backups.Models.RepositoryObjects;

public class DirectoryRepositoryObject : IDirectoryRepositoryObject
{
    private readonly Func<IReadOnlyCollection<IRepositoryObject>> _func;

    public DirectoryRepositoryObject(string name, Func<IReadOnlyCollection<IRepositoryObject>> func)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(func);

        Name = name;
        _func = func;
    }

    public IReadOnlyCollection<IRepositoryObject> Children => _func.Invoke();

    public string Name { get; }

    public void Accept(IRepositoryObjectVisitor repositoryObjectVisitor)
    {
        repositoryObjectVisitor.Visit(this);
    }

    public override string ToString()
    {
        return $"Repository directory {Name}";
    }
}