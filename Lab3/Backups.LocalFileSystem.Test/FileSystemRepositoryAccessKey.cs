using Backups.Models;

namespace Backups.LocalFileSystem.Test;

public class FileSystemRepositoryAccessKey : IRepositoryAccessKey
{
    public FileSystemRepositoryAccessKey(string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        Value = value;
    }

    public string Value { get; }

    public IRepositoryAccessKey CombineWithSeparator(IRepositoryAccessKey other)
    {
        return CombineWithSeparator(other.Value);
    }

    public IRepositoryAccessKey CombineWithSeparator(string value)
    {
        return new FileSystemRepositoryAccessKey(Path.Combine(Value, value));
    }

    public IRepositoryAccessKey CombineWithExtension(string extension)
    {
        return new FileSystemRepositoryAccessKey(Path.ChangeExtension(Value, extension));
    }
}