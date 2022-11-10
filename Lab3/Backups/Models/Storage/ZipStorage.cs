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
    private readonly IEnumerable<IArchivedObject> _archivedObjects;

    public ZipStorage(IRepository repository, IRepositoryAccessKey accessKey, IEnumerable<IArchivedObject> objects)
    {
        ArgumentNullException.ThrowIfNull(repository);
        ArgumentNullException.ThrowIfNull(accessKey);
        ArgumentNullException.ThrowIfNull(objects);

        AccessKey = accessKey;
        Repository = repository;
        _archivedObjects = objects;
    }

    public IRepositoryAccessKey AccessKey { get; }
    public IRepository Repository { get; }
    public IEnumerable<IRepositoryObject> Objects => GetObjects();

    public override string ToString()
    {
        return $"ZipStorage {AccessKey} saved into {Repository}";
    }

    private IEnumerable<IRepositoryObject> GetObjects()
    {
        var objects = new List<IRepositoryObject>();
        var zipArch = new ZipArchive(
            ((IFileRepositoryObject)Repository.GetComponent(AccessKey)).Stream,
            ZipArchiveMode.Read);

        foreach (IArchivedObject archivedObject in _archivedObjects)
        {
            ZipArchiveEntry? entry = zipArch.GetEntry(archivedObject.Name);
            if (entry is null)
                throw ZipStorageException.ZipEntryNotFound(archivedObject.Name);

            objects.Add(archivedObject.GetRepositoryObject(entry));
        }

        return objects;
    }
}