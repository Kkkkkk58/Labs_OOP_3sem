using Backups.Models.Abstractions;

namespace Backups.Test;

public class InMemoryRepositoryAccessKey : IRepositoryAccessKey
{
    private const string InMemorySeparator = "/";

    public InMemoryRepositoryAccessKey(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public IRepositoryAccessKey Combine(IRepositoryAccessKey other)
    {
        return Combine(other.Value);
    }

    public IRepositoryAccessKey Combine(string value)
    {
        return new InMemoryRepositoryAccessKey(Value + InMemorySeparator + value);
    }

    public IRepositoryAccessKey ApplyExtension(string extension)
    {
        return new InMemoryRepositoryAccessKey(Path.ChangeExtension(Value, extension));
    }
}