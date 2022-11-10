using System.IO.Compression;
using Backups.Models.Abstractions;
using Backups.Models.ArchivedObjects;
using Backups.Models.ArchivedObjects.Abstractions;
using Backups.Models.RepositoryObjects.Abstractions;

namespace Backups.Tools.Archiver;

public class ZipArchiveVisitor : IRepositoryObjectVisitor
{
    private readonly Stack<ZipArchive> _archives;
    private readonly Stack<List<IArchivedObject>> _archivedObjects;

    public ZipArchiveVisitor(ZipArchive zipArchive)
    {
        ArgumentNullException.ThrowIfNull(zipArchive);

        _archives = new Stack<ZipArchive>(new[] { zipArchive });
        _archivedObjects = new Stack<List<IArchivedObject>>(1);
        _archivedObjects.Push(new List<IArchivedObject>());
    }

    public void Visit(IFileRepositoryObject fileRepositoryObject)
    {
        ArgumentNullException.ThrowIfNull(fileRepositoryObject);

        ZipArchiveEntry entry = GetArchiveEntry(fileRepositoryObject);
        using Stream archiveEntryStream = entry.Open();
        using (Stream data = fileRepositoryObject.Stream)
        {
            data.CopyTo(archiveEntryStream);
        }

        _archivedObjects.Peek().Add(new ArchivedFile(fileRepositoryObject.Name));
    }

    public void Visit(IDirectoryRepositoryObject directoryRepositoryObject)
    {
        ArgumentNullException.ThrowIfNull(directoryRepositoryObject);

        using Stream archiveEntryStream = GetArchiveEntry(directoryRepositoryObject).Open();
        using var directoryArchive = new ZipArchive(archiveEntryStream, ZipArchiveMode.Create);

        _archives.Push(directoryArchive);
        _archivedObjects.Push(new List<IArchivedObject>());
        foreach (IRepositoryObject repositoryObject in directoryRepositoryObject.Children)
        {
            repositoryObject.Accept(this);
        }

        _archives.Pop();
        List<IArchivedObject> curList = _archivedObjects.Pop();
        _archivedObjects.Peek().Add(new ArchivedFolder(directoryRepositoryObject.Name, curList));
    }

    public IEnumerable<IArchivedObject> GetArchivedObjects()
    {
        return _archivedObjects.Peek();
    }

    private ZipArchiveEntry GetArchiveEntry(IDirectoryRepositoryObject repositoryObject)
    {
        ZipArchive archive = _archives.Peek();
        return archive.CreateEntry($"{repositoryObject.Name}.zip");
    }

    private ZipArchiveEntry GetArchiveEntry(IFileRepositoryObject fileObject)
    {
        ZipArchive archive = _archives.Peek();
        return archive.CreateEntry(fileObject.Name);
    }
}