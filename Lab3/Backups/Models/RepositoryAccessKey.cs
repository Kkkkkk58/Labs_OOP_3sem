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
        ArgumentNullException.ThrowIfNull(keyParts);
        ArgumentNullException.ThrowIfNull(keySeparator);

        KeyParts = keyParts.ToList();
        _keySeparator = keySeparator;
    }

    public string FullKey => string.Join(_keySeparator, KeyParts);
    public string Name => KeyParts.Last();
    public IReadOnlyCollection<string> KeyParts { get; }

    public IRepositoryAccessKey Combine(IRepositoryAccessKey other)
    {
        ArgumentNullException.ThrowIfNull(other);
        return new RepositoryAccessKey(KeyParts.Concat(other.KeyParts), _keySeparator);
    }

    public IRepositoryAccessKey Combine(string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        var additionalKey = new RepositoryAccessKey(new[] { value }, _keySeparator);
        return Combine(additionalKey);
    }

    public IRepositoryAccessKey ApplyExtension(string extension)
    {
        ArgumentNullException.ThrowIfNull(extension);

        string nameWithExtension = $"{Name}.{extension}";
        IEnumerable<string> partsWithoutName = KeyParts.SkipLast(1);

        IEnumerable<string> newKeyParts = partsWithoutName.Concat(new[] { nameWithExtension });
        return new RepositoryAccessKey(newKeyParts, _keySeparator);
    }

    public override string ToString()
    {
        return FullKey;
    }
}