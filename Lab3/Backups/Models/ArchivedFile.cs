using System.IO.Compression;
using Backups.Models.Abstractions;

namespace Backups.Models;

public class ArchivedFile : IArchivedFile
{
    public ArchivedFile(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public IRepositoryObject GetRepositoryObject(ZipArchiveEntry entry)
    {
        return new FileRepositoryObject(Name, entry.Open);
    }
}