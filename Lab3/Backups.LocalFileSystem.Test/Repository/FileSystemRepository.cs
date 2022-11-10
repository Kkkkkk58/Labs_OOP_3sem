using Backups.Models;
using Backups.Models.Abstractions;
using Backups.Models.Repository.Abstractions;
using Backups.Models.RepositoryObjects;

namespace Backups.LocalFileSystem.Test.Repository;

public class FileSystemRepository : IRepository
{
    public FileSystemRepository(string baseDirectoryPath)
    {
        if (!Directory.Exists(baseDirectoryPath))
            throw new ArgumentException($"Invalid base directory: {baseDirectoryPath}");

        string separator = new string(Path.DirectorySeparatorChar, 1);
        BaseKey = new RepositoryAccessKey(baseDirectoryPath, separator);
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
            return GetFileComponent(accessKey);
        if (ContainsDirectory(path))
            return GetDirectoryComponent(accessKey);
        throw new ArgumentException($"Invalid path: {accessKey}");
    }

    public Stream OpenWrite(IRepositoryAccessKey accessKey)
    {
        ArgumentNullException.ThrowIfNull(accessKey);

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

    private IRepositoryObject GetFileComponent(IRepositoryAccessKey accessKey)
    {
        return new FileRepositoryObject(accessKey.Name, () => GetStream(accessKey));
    }

    private Stream GetStream(IRepositoryAccessKey accessKey)
    {
        return File.OpenRead(GetPath(accessKey));
    }

    private IRepositoryObject GetDirectoryComponent(IRepositoryAccessKey accessKey)
    {
        return new DirectoryRepositoryObject(accessKey.Name, () => GetObjects(accessKey));
    }

    private IReadOnlyCollection<IRepositoryObject> GetObjects(IRepositoryAccessKey accessKey)
    {
        var directoryInfo = new DirectoryInfo(GetPath(accessKey));
        IEnumerable<FileSystemInfo> infos = directoryInfo.EnumerateFileSystemInfos();

        var objects = new List<IRepositoryObject>();
        foreach (FileSystemInfo fileSystemInfo in infos)
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
}