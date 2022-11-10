using System.IO.Compression;
using Backups.Models.Abstractions;

namespace Backups.Models.ArchivedObjects.Abstractions;

public interface IArchivedObject
{
    string Name { get; }
    IRepositoryObject GetRepositoryObject(ZipArchiveEntry entry);
}