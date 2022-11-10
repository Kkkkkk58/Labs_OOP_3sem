using System.IO.Compression;
using Backups.Exceptions;
using Backups.Models.ArchivedObjects.Abstractions;
using Backups.Models.RepositoryObjects;
using Backups.Models.RepositoryObjects.Abstractions;

namespace Backups.Models.ArchivedObjects;

public class ArchivedDirectory : IArchivedDirectory
{
    public ArchivedDirectory(string name, IEnumerable<IArchivedObject> children)
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

    public override string ToString()
    {
        return $"Archived directory {Name}";
    }

    private IReadOnlyCollection<IRepositoryObject> GetObjects(ZipArchiveEntry entry)
    {
        ArgumentNullException.ThrowIfNull(entry);

        var archive = new ZipArchive(entry.Open(), ZipArchiveMode.Read);

        var objects = new List<IRepositoryObject>();
        foreach (IArchivedObject archivedObject in Children)
        {
            ZipArchiveEntry? childEntry = archive.GetEntry(archivedObject.Name);
            if (childEntry is null)
                throw ArchivedFolderException.ChildEntryNotFound(archivedObject.Name);

            objects.Add(archivedObject.GetRepositoryObject(childEntry));
        }

        return objects.AsReadOnly();
    }
}