using System.IO.Compression;
using Backups.Models.Abstractions;
using Backups.Models.ArchivedObjects.Abstractions;
using Backups.Models.RepositoryObjects;

namespace Backups.Models.ArchivedObjects;

public class ArchivedFile : IArchivedFile
{
    public ArchivedFile(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public IRepositoryObject GetRepositoryObject(ZipArchiveEntry entry)
    {
        ArgumentNullException.ThrowIfNull(entry);

        return new FileRepositoryObject(Name, entry.Open);
    }

    public override string ToString()
    {
        return $"Archived file {Name}";
    }
}