using Backups.Models.Abstractions;

namespace Backups.Models;

public class RepositoryAccessKey : IRepositoryAccessKey
{
    private readonly string _keySeparator;

    public RepositoryAccessKey(string baseKey, string keySeparator)
        : this(baseKey.Split(keySeparator), keySeparator)
    {
    }

    public RepositoryAccessKey(IEnumerable<string> keyParts, string keySeparator)
    {
        KeyParts = keyParts;
        _keySeparator = keySeparator;
    }

    public string FullKey => string.Join(_keySeparator, KeyParts);
    public string Name => KeyParts.Last();
    public IEnumerable<string> KeyParts { get; }

    public IRepositoryAccessKey Combine(IRepositoryAccessKey other)
    {
        return new RepositoryAccessKey(KeyParts.Concat(other.KeyParts), _keySeparator);
    }

    public IRepositoryAccessKey Combine(string value)
    {
        return Combine(new RepositoryAccessKey(new[] { value }, _keySeparator));
    }

    public IRepositoryAccessKey ApplyExtension(string extension)
    {
        string nameWithExtension = $"{Name}.{extension}";
        return new RepositoryAccessKey(KeyParts.Take(KeyParts.Count() - 1).Concat(new[] { nameWithExtension }), _keySeparator);
    }
}