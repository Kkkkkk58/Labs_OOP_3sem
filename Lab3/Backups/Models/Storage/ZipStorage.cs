using System.IO.Compression;
using Backups.Exceptions;
using Backups.Models.Abstractions;
using Backups.Models.ArchivedObjects.Abstractions;
using Backups.Models.Repository.Abstractions;
using Backups.Models.RepositoryObjects.Abstractions;
using Backups.Models.Storage.Abstractions;

namespace Backups.Models.Storage;

public class ZipStorage : IStorage
{
    private readonly IReadOnlyCollection<IArchivedObject> _archivedObjects;

    public ZipStorage(IRepository repository, IRepositoryAccessKey accessKey, IEnumerable<IArchivedObject> objects)
    {
        ArgumentNullException.ThrowIfNull(repository);
        ArgumentNullException.ThrowIfNull(accessKey);
        ArgumentNullException.ThrowIfNull(objects);

        AccessKey = accessKey;
        Repository = repository;
        _archivedObjects = objects.ToList();
    }

    public IRepositoryAccessKey AccessKey { get; }
    public IRepository Repository { get; }
    public IReadOnlyCollection<IRepositoryObject> Objects => GetRepositoryObjects();

    public override string ToString()
    {
        return $"ZipStorage {AccessKey} saved into {Repository}";
    }

    private IReadOnlyCollection<IRepositoryObject> GetRepositoryObjects()
    {
        IRepositoryObject archiveRepoObject = Repository.GetComponent(AccessKey);
        if (archiveRepoObject is not IFileRepositoryObject archiveFile)
            throw ZipStorageException.InvalidRepositoryObject();

        var zipArchive = new ZipArchive(archiveFile.Stream, ZipArchiveMode.Read);

        return GetObjectsFromArchive(zipArchive);
    }

    private IReadOnlyCollection<IRepositoryObject> GetObjectsFromArchive(ZipArchive zipArchive)
    {
        var objects = new List<IRepositoryObject>();
        foreach (IArchivedObject archivedObject in _archivedObjects)
        {
            ZipArchiveEntry? entry = zipArchive.GetEntry(archivedObject.Name);
            if (entry is null)
                throw ZipStorageException.ZipEntryNotFound(archivedObject.Name);

            objects.Add(archivedObject.GetRepositoryObject(entry));
        }

        return objects.AsReadOnly();
    }
}