using System.IO.Compression;

namespace Backups.Models.Abstractions;

public interface IArchivedObject
{
    string Name { get; }
    IRepositoryObject GetRepositoryObject(ZipArchiveEntry entry);
}