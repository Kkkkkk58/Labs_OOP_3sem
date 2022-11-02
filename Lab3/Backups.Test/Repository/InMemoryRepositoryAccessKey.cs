using Backups.Models.Abstractions;

namespace Backups.Test.Repository;

public class InMemoryRepositoryAccessKey : IRepositoryAccessKey
{
    private const char InMemorySeparator = '/';

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
        string newKey = string.Join(InMemorySeparator, Value, value);
        return new InMemoryRepositoryAccessKey(newKey);
    }

    public IRepositoryAccessKey ApplyExtension(string extension)
    {
        return new InMemoryRepositoryAccessKey(Path.ChangeExtension(Value, extension));
    }
}