using Backups.Models;
using Backups.Models.Abstractions;
using Zio;
using Zio.FileSystems;

namespace Backups.Test;

public class InMemoryRepository : IRepository
{
    private readonly MemoryFileSystem _fileSystem;

    public InMemoryRepository(MemoryFileSystem fileSystem, string baseDirectory)
    {
        if (!fileSystem.DirectoryExists(baseDirectory))
            throw new NotImplementedException();

        _fileSystem = fileSystem;
        BaseKey = new InMemoryRepositoryAccessKey(baseDirectory);
    }

    public IRepositoryAccessKey BaseKey { get; }

    public bool Contains(IRepositoryAccessKey accessKey)
    {
        string path = GetPath(accessKey);
        return ContainsFile(path) || ContainsDirectory(path);
    }

    public IReadOnlyList<RepositoryObject> GetData(IRepositoryAccessKey accessKey)
    {
        string path = GetPath(accessKey);
        if (ContainsFile(path))
            return GetDataFromFile(path, accessKey);
        if (ContainsDirectory(path))
            return GetDataFromDirectory(path, accessKey);
        throw new NotImplementedException();
    }

    public Stream OpenStream(IRepositoryAccessKey accessKey)
    {
        UPath p = GetPath(accessKey);
        UPath dirname = p.GetDirectory();
        _fileSystem.CreateDirectory(dirname);
        return _fileSystem.OpenFile(p, FileMode.Create, FileAccess.Write);
    }

    private IReadOnlyList<RepositoryObject> GetDataFromFile(string path, IRepositoryAccessKey accessKey)
    {
        var fileRepositoryObject = new RepositoryObject(accessKey, _fileSystem.OpenFile(path, FileMode.Open, FileAccess.Read));
        return new List<RepositoryObject> { fileRepositoryObject };
    }

    private IReadOnlyList<RepositoryObject> GetDataFromDirectory(string path, IRepositoryAccessKey accessKey)
    {
        var data = new List<RepositoryObject>();
        DirectoryEntry directoryInfo = _fileSystem.GetDirectoryEntry(path);
        foreach (FileEntry fileInfo in directoryInfo.EnumerateFiles())
        {
            string name = Path.GetRelativePath(path, fileInfo.FullName);
            IRepositoryAccessKey fileAccessKey = accessKey.CombineWithSeparator(name);
            var content = new RepositoryObject(fileAccessKey, _fileSystem.OpenFile(fileInfo.FullName, FileMode.Open, FileAccess.Read));
            data.Add(content);
        }

        return data;
    }

    private string GetPath(IRepositoryAccessKey accessKey)
    {
        return BaseKey.CombineWithSeparator(accessKey).Value;
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