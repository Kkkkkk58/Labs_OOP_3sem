using Backups.Models;
using Backups.Models.Abstractions;
using Backups.Models.Repository.Abstractions;
using Backups.Models.RepositoryObjects;
using Backups.Models.RepositoryObjects.Abstractions;
using Zio;
using Zio.FileSystems;

namespace Backups.Test.Repository;

public class InMemoryRepository : IRepository
{
    private readonly MemoryFileSystem _fileSystem;

    public InMemoryRepository(MemoryFileSystem fileSystem, string baseDirectory, string inMemorySeparator = "/")
    {
        ArgumentNullException.ThrowIfNull(fileSystem);
        if (!fileSystem.DirectoryExists(baseDirectory))
            throw new ArgumentException($"Invalid base directory: {baseDirectory}");

        _fileSystem = fileSystem;
        BaseKey = new RepositoryAccessKey(baseDirectory, inMemorySeparator);
    }

    public IRepositoryAccessKey BaseKey { get; }

    public bool Contains(IRepositoryAccessKey accessKey)
    {
        ArgumentNullException.ThrowIfNull(accessKey);

        UPath path = GetPath(accessKey);
        return ContainsFile(path) || ContainsDirectory(path);
    }

    public IRepositoryObject GetComponent(IRepositoryAccessKey accessKey)
    {
        ArgumentNullException.ThrowIfNull(accessKey);

        UPath path = GetPath(accessKey);
        if (ContainsFile(path))
            return GetDataFromFile(accessKey);
        if (ContainsDirectory(path))
            return GetDataFromDirectory(accessKey);

        throw new ArgumentException($"Invalid path: {accessKey}");
    }

    public Stream OpenWrite(IRepositoryAccessKey accessKey)
    {
        ArgumentNullException.ThrowIfNull(accessKey);

        UPath p = GetPath(accessKey);
        UPath dirname = p.GetDirectory();
        _fileSystem.CreateDirectory(dirname);
        return _fileSystem.OpenFile(p, FileMode.Create, FileAccess.Write);
    }

    private IRepositoryObject GetDataFromFile(IRepositoryAccessKey accessKey)
    {
        return new FileRepositoryObject(accessKey.Name, () => GetStream(accessKey));
    }

    private Stream GetStream(IRepositoryAccessKey accessKey)
    {
        return _fileSystem.OpenFile(GetPath(accessKey), FileMode.Open, FileAccess.Read);
    }

    private IRepositoryObject GetDataFromDirectory(IRepositoryAccessKey accessKey)
    {
        return new DirectoryRepositoryObject(accessKey.Name, () => GetObjects(accessKey));
    }

    private IReadOnlyCollection<IRepositoryObject> GetObjects(IRepositoryAccessKey accessKey)
    {
        DirectoryEntry directoryEntry = _fileSystem.GetDirectoryEntry(GetPath(accessKey));
        IEnumerable<FileSystemEntry> fsObjects = directoryEntry.EnumerateEntries();

        return fsObjects
            .Select(fsObject => GetInnerObject(accessKey, fsObject))
            .ToList()
            .AsReadOnly();
    }

    private IRepositoryObject GetInnerObject(IRepositoryAccessKey accessKey, FileSystemEntry fsObject)
    {
        IRepositoryAccessKey objectKey = accessKey.Combine(fsObject.Name);

        if (ContainsDirectory(fsObject.FullName))
            return new DirectoryRepositoryObject(objectKey.Name, () => GetObjects(objectKey));

        if (ContainsFile(fsObject.FullName))
            return new FileRepositoryObject(objectKey.Name, () => GetStream(objectKey));

        throw new ArgumentException($"An object {objectKey} doesn't exist");
    }

    private bool ContainsFile(UPath path)
    {
        return _fileSystem.FileExists(path);
    }

    private bool ContainsDirectory(UPath path)
    {
        return _fileSystem.DirectoryExists(path);
    }

    private UPath GetPath(IRepositoryAccessKey accessKey)
    {
        return accessKey.FullKey.StartsWith(BaseKey.FullKey) ? accessKey.FullKey : BaseKey.Combine(accessKey).FullKey;
    }
}