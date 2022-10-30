using Backups.Models.Abstractions;

namespace Backups.Models;

public record RestorePointVersion : IRestorePointVersion
{
    public RestorePointVersion(int value = 0)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value));

        Value = value;
    }

    public int Value { get; }

    public override string ToString()
    {
        return $"v{Value}";
    }

    public IRestorePointVersion GetNext()
    {
        return new RestorePointVersion(Value + 1);
    }
}