using Backups.Models.Abstractions;

namespace Backups.Models;

public class FileRepositoryObject : IFileRepositoryObject
{
    private readonly Func<Stream> _func;

    public FileRepositoryObject(string name, Func<Stream> func)
    {
        Name = name;
        _func = func;
    }

    public Stream Stream => _func.Invoke();

    public string Name { get; }

    public void Accept(IRepositoryObjectVisitor repositoryObjectVisitor)
    {
        repositoryObjectVisitor.Visit(this);
    }
}