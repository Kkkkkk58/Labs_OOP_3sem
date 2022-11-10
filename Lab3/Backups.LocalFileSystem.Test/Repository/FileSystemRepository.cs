using Backups.Models;
using Backups.Models.Abstractions;
using Backups.Models.Repository.Abstractions;
using Backups.Models.RepositoryObjects;
using Backups.Models.RepositoryObjects.Abstractions;

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
            return new FileRepositoryObject(accessKey.Name, () => GetStream(accessKey));
        if (ContainsDirectory(path))
            return new DirectoryRepositoryObject(accessKey.Name, () => GetObjects(accessKey));

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

    private Stream GetStream(IRepositoryAccessKey accessKey)
    {
        return File.OpenRead(GetPath(accessKey));
    }

    private IReadOnlyCollection<IRepositoryObject> GetObjects(IRepositoryAccessKey accessKey)
    {
        var directoryInfo = new DirectoryInfo(GetPath(accessKey));
        IEnumerable<FileSystemInfo> fsObjects = directoryInfo.EnumerateFileSystemInfos();

        var repositoryObjects = new List<IRepositoryObject>();
        foreach (FileSystemInfo fsObject in fsObjects)
        {
            IRepositoryAccessKey objectKey = accessKey.Combine(fsObject.Name);
            if (ContainsDirectory(fsObject.FullName))
            {
                repositoryObjects.Add(new DirectoryRepositoryObject(objectKey.Name, () => GetObjects(objectKey)));
            }
            else if (ContainsFile(fsObject.FullName))
            {
                repositoryObjects.Add(new FileRepositoryObject(objectKey.Name, () => GetStream(objectKey)));
            }
            else
            {
                throw new ArgumentException($"Repository doesn't contain object {objectKey}");
            }
        }

        return repositoryObjects.AsReadOnly();
    }

    private string GetPath(IRepositoryAccessKey accessKey)
    {
        return accessKey.FullKey.StartsWith(BaseKey.FullKey) ? accessKey.FullKey : BaseKey.Combine(accessKey).FullKey;
    }
}