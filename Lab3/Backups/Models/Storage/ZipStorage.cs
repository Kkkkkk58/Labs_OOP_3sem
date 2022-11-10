using System.IO.Compression;
using Backups.Models.Abstractions;
using Backups.Models.Storage.Abstractions;

namespace Backups.Models.Storage;

public class ZipStorage : IStorage
{
    private readonly IEnumerable<IArchivedObject> _archivedObjects;

    public ZipStorage(IRepositoryAccessKey accessKey, IRepository repository, IEnumerable<IArchivedObject> objects)
    {
        AccessKey = accessKey;
        Repository = repository;
        _archivedObjects = objects;
    }

    public IRepositoryAccessKey AccessKey { get; }
    public IRepository Repository { get; }
    public IEnumerable<IRepositoryObject> Objects => GetObjects();

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
                continue;

            objects.Add(archivedObject.GetRepositoryObject(entry));
        }

        return objects;
    }
}