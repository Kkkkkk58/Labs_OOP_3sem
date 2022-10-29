namespace Backups.Models;

public record RepositoryAccessKey
{
    public RepositoryAccessKey(string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        Value = value;
    }

    public string Value { get; }
}