using Backups.Models;
using Backups.Models.Abstractions;

namespace Backups.LocalFileSystem.Test;

public class FileSystemRepository : IRepository
{
    public FileSystemRepository(string baseDirectoryPath)
    {
        if (!Directory.Exists(baseDirectoryPath))
            throw new NotImplementedException();
        BaseKey = new FileSystemRepositoryAccessKey(baseDirectoryPath);
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
            return GetDataFromFile(accessKey, path);
        if (ContainsDirectory(path))
            return GetDataFromDirectory(accessKey, path);
        throw new NotImplementedException();
    }

    public Stream OpenStream(IRepositoryAccessKey accessKey)
    {
        string path = GetPath(accessKey);
        string? dirname = Path.GetDirectoryName(path);
        if (dirname is not null)
        {
            Directory.CreateDirectory(dirname);
        }

        return File.OpenWrite(path);
    }

    private static bool ContainsFile(string path)
    {
        return File.Exists(path);
    }

    private static bool ContainsDirectory(string path)
    {
        return Directory.Exists(path);
    }

    private static IReadOnlyList<IRepositoryObject> GetDataFromDirectory(IRepositoryAccessKey accessKey, string path)
    {
        var data = new List<IRepositoryObject>();
        var directoryInfo = new DirectoryInfo(path);
        foreach (FileInfo fileInfo in directoryInfo.EnumerateFiles())
        {
            string name = Path.GetRelativePath(directoryInfo.FullName, fileInfo.FullName);
            IRepositoryAccessKey fileAccessKey =
                accessKey.Combine(new FileSystemRepositoryAccessKey(name));
            var content = new RepositoryObject(fileAccessKey, File.OpenRead(fileInfo.FullName));
            data.Add(content);
        }

        return data;
    }

    private static IReadOnlyList<IRepositoryObject> GetDataFromFile(IRepositoryAccessKey accessKey, string path)
    {
        var fileRepositoryObject = new RepositoryObject(accessKey, File.OpenRead(path));
        return new List<IRepositoryObject> { fileRepositoryObject };
    }

    private string GetPath(IRepositoryAccessKey accessKey)
    {
        return BaseKey.Combine(accessKey).Value;
    }
}