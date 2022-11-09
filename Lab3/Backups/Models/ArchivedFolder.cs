using System.IO.Compression;
using Backups.Models.Abstractions;

namespace Backups.Models;

public class ArchivedFolder : IArchivedFolder
{
    public ArchivedFolder(string name, IEnumerable<IArchivedObject> children)
    {
        Name = name;
        Children = children;
    }

    public IEnumerable<IArchivedObject> Children { get; }

    public string Name { get; }

    public IRepositoryObject GetRepositoryObject(ZipArchiveEntry entry)
    {
        return new DirectoryRepositoryObject(Name, () => GetObjects(entry));
    }

    private IReadOnlyCollection<IRepositoryObject> GetObjects(ZipArchiveEntry entry)
    {
        var objects = new List<IRepositoryObject>();
        var archive = new ZipArchive(entry.Open(), ZipArchiveMode.Read);
        foreach (IArchivedObject archivedObject in Children)
        {
            ZipArchiveEntry? childEntry = archive.GetEntry(archivedObject.Name);
            if (childEntry is null)
                throw new NotImplementedException();

            objects.Add(archivedObject.GetRepositoryObject(childEntry));
        }

        return objects.AsReadOnly();
    }
}