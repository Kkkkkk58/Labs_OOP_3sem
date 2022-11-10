using Backups.Models.Abstractions;
using Backups.Models.RepositoryObjects.Abstractions;

namespace Backups.Models.RepositoryObjects;

public class FileRepositoryObject : IFileRepositoryObject
{
    private readonly Func<Stream> _func;

    public FileRepositoryObject(string name, Func<Stream> func)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(func);

        Name = name;
        _func = func;
    }

    public Stream Stream => _func.Invoke();

    public string Name { get; }

    public void Accept(IRepositoryObjectVisitor repositoryObjectVisitor)
    {
        repositoryObjectVisitor.Visit(this);
    }

    public override string ToString()
    {
        return $"Repository file {Name}";
    }
}