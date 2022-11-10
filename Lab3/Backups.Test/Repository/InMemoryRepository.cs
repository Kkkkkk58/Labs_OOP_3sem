using Backups.Models;
using Backups.Models.Abstractions;
using Backups.Models.Repository.Abstractions;
using Backups.Models.RepositoryObjects;
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

        string path = GetPath(accessKey);
        return ContainsFile(path) || ContainsDirectory(path);
    }

    public IRepositoryObject GetComponent(IRepositoryAccessKey accessKey)
    {
        ArgumentNullException.ThrowIfNull(accessKey);

        string path = GetPath(accessKey);
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
        DirectoryEntry directoryInfo = _fileSystem.GetDirectoryEntry(GetPath(accessKey));
        IEnumerable<FileSystemEntry> infos = directoryInfo.EnumerateEntries();

        var objects = new List<IRepositoryObject>();
        foreach (FileSystemEntry fileSystemInfo in infos)
        {
            IRepositoryAccessKey objectKey = accessKey.Combine(fileSystemInfo.Name);
            if (ContainsDirectory(fileSystemInfo.FullName))
            {
                objects.Add(new DirectoryRepositoryObject(objectKey.Name, () => GetObjects(objectKey)));
            }
            else if (ContainsFile(fileSystemInfo.FullName))
            {
                objects.Add(new FileRepositoryObject(objectKey.Name, () => GetStream(objectKey)));
            }
        }

        return objects.AsReadOnly();
    }

    private string GetPath(IRepositoryAccessKey accessKey)
    {
        return accessKey.FullKey.StartsWith(BaseKey.FullKey) ? accessKey.FullKey : BaseKey.Combine(accessKey).FullKey;
    }

    private bool ContainsFile(UPath path)
    {
        return _fileSystem.FileExists(path);
    }

    private bool ContainsDirectory(UPath path)
    {
        return _fileSystem.DirectoryExists(path);
    }
}