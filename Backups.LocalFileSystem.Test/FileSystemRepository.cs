using Backups.Models;
using Backups.Models.Abstractions;

namespace Backups.LocalFileSystem.Test;

public class FileSystemRepository : IRepository
{
    private readonly DirectoryInfo _baseDirectoryInfo;

    public FileSystemRepository(DirectoryInfo baseDirectoryInfo)
    {
        if (baseDirectoryInfo is null || !baseDirectoryInfo.Exists)
            throw new ArgumentNullException(nameof(baseDirectoryInfo));

        _baseDirectoryInfo = baseDirectoryInfo;
    }

    public bool Contains(RepositoryAccessKey accessKey)
    {
        string path = GetPath(accessKey);
        return File.Exists(path) || Directory.Exists(path);
    }

    public Stream GetData(RepositoryAccessKey accessKey)
    {
        string path = GetPath(accessKey);
        return File.OpenRead(path);
    }

    public void SaveData(RepositoryAccessKey accessKey, Stream content)
    {
        string path = GetPath(accessKey);
        using FileStream fs = File.Create(path);
        content.Seek(0, SeekOrigin.Begin);
        content.CopyTo(fs);
    }

    private string GetPath(RepositoryAccessKey accessKey)
    {
        return Path.Combine(_baseDirectoryInfo.FullName, accessKey.Value);
    }
}