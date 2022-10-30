using Backups.Models.Abstractions;

namespace Backups.LocalFileSystem.Test;

public class FileSystemRepositoryAccessKey : IRepositoryAccessKey
{
    public FileSystemRepositoryAccessKey(string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        Value = value;
    }

    public string Value { get; }

    public IRepositoryAccessKey Combine(IRepositoryAccessKey other)
    {
        return Combine(other.Value);
    }

    public IRepositoryAccessKey Combine(string value)
    {
        return new FileSystemRepositoryAccessKey(Path.Combine(Value, value));
    }

    public IRepositoryAccessKey ApplyExtension(string extension)
    {
        return new FileSystemRepositoryAccessKey(Path.ChangeExtension(Value, extension));
    }
}