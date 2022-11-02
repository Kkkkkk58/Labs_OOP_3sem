using Backups.Models;
using Backups.Models.Abstractions;
using Zio;
using Zio.FileSystems;

namespace Backups.Test.Repository;

public class InMemoryRepository : IRepository
{
    private readonly MemoryFileSystem _fileSystem;

    public InMemoryRepository(MemoryFileSystem fileSystem, string baseDirectory)
    {
        ArgumentNullException.ThrowIfNull(fileSystem);
        if (!fileSystem.DirectoryExists(baseDirectory))
            throw new ArgumentException($"Invalid base directory: {baseDirectory}");

        _fileSystem = fileSystem;
        BaseKey = new InMemoryRepositoryAccessKey(baseDirectory);
    }

    public IRepositoryAccessKey BaseKey { get; }

    public bool Contains(IRepositoryAccessKey accessKey)
    {
        string path = GetPath(accessKey);
        return ContainsFile(path) || ContainsDirectory(path);
    }

    public IReadOnlyList<IRepositoryObject> GetData(IRepositoryAccessKey accessKey)
    {
        string path = GetPath(accessKey);
        if (ContainsFile(path))
            return GetDataFromFile(path, accessKey);
        if (ContainsDirectory(path))
            return GetDataFromDirectory(path, accessKey);
        throw new ArgumentException($"Invalid path: {accessKey}");
    }

    public Stream OpenStream(IRepositoryAccessKey accessKey)
    {
        UPath p = GetPath(accessKey);
        UPath dirname = p.GetDirectory();
        _fileSystem.CreateDirectory(dirname);
        return _fileSystem.OpenFile(p, FileMode.Create, FileAccess.Write);
    }

    private IReadOnlyList<IRepositoryObject> GetDataFromFile(string path, IRepositoryAccessKey accessKey)
    {
        var fileRepositoryObject =
            new RepositoryObject(accessKey, _fileSystem.OpenFile(path, FileMode.Open, FileAccess.Read));
        return new List<IRepositoryObject> { fileRepositoryObject };
    }

    private IReadOnlyList<IRepositoryObject> GetDataFromDirectory(string path, IRepositoryAccessKey accessKey)
    {
        var data = new List<IRepositoryObject>();
        DirectoryEntry directoryInfo = _fileSystem.GetDirectoryEntry(path);
        foreach (FileEntry fileInfo in directoryInfo.EnumerateFiles())
        {
            string name = Path.GetRelativePath(path, fileInfo.FullName);
            IRepositoryAccessKey fileAccessKey = accessKey.Combine(name);
            Stream stream = _fileSystem.OpenFile(fileInfo.FullName, FileMode.Open, FileAccess.Read);
            var content = new RepositoryObject(fileAccessKey, stream);
            data.Add(content);
        }

        return data;
    }

    private string GetPath(IRepositoryAccessKey accessKey)
    {
        return BaseKey.Combine(accessKey).Value;
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