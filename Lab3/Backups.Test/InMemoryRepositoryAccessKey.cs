using Backups.Models;

namespace Backups.Test;

public class InMemoryRepositoryAccessKey : IRepositoryAccessKey
{
    private const string InMemorySeparator = "/";
    public InMemoryRepositoryAccessKey(string value)
    {
        Value = value;
    }

    public string Value { get; }
    public IRepositoryAccessKey CombineWithSeparator(IRepositoryAccessKey other)
    {
        return CombineWithSeparator(other.Value);
    }

    public IRepositoryAccessKey CombineWithSeparator(string value)
    {
        return new InMemoryRepositoryAccessKey(Value + InMemorySeparator + value);
    }

    public IRepositoryAccessKey CombineWithExtension(string extension)
    {
        return new InMemoryRepositoryAccessKey(Path.ChangeExtension(Value, extension));
    }
}