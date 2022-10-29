using Backups.Models;
using Backups.Models.Abstractions;
using Zio;
using Zio.FileSystems;

namespace Backups.Test;

public class InMemoryRepository : IRepository
{
    private readonly MemoryFileSystem _fileSystem;

    public InMemoryRepository(MemoryFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public bool Contains(RepositoryAccessKey accessKey)
    {
        return _fileSystem.FileExists(accessKey.Value) || _fileSystem.DirectoryExists(accessKey.Value);
    }

    public Stream GetData(RepositoryAccessKey accessKey)
    {
        return _fileSystem.OpenFile(accessKey.Value, FileMode.Open, FileAccess.Read);
    }

    public void SaveData(RepositoryAccessKey accessKey, Stream content)
    {
        using Stream fs = _fileSystem.CreateFile(accessKey.Value);
        content.Seek(0, SeekOrigin.Begin);
        content.CopyTo(fs);
    }
}