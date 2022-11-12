using Backups.Entities.Abstractions;
using Backups.Exceptions;
using Backups.Models.Abstractions;

namespace Backups.Entities;

public class Backup : IBackup, IEquatable<Backup>
{
    private readonly List<IRestorePoint> _restorePoints;

    public Backup(string name)
        : this(Guid.NewGuid(), name, new List<IRestorePoint>())
    {
    }

    public Backup(Guid id, string name, IEnumerable<IRestorePoint> restorePoints)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(restorePoints);

        Id = id;
        Name = name;
        _restorePoints = restorePoints
            .OrderBy(restorePoint => restorePoint.CreationDate)
            .ToList();
    }

    public Guid Id { get; }
    public string Name { get; }
    public IReadOnlyCollection<IRestorePoint> RestorePoints => _restorePoints.AsReadOnly();

    public IRestorePoint AddRestorePoint(IRestorePoint restorePoint)
    {
        ArgumentNullException.ThrowIfNull(restorePoint);
        if (_restorePoints.Contains(restorePoint))
            throw BackupException.RestorePointAlreadyExists(restorePoint, this);

        _restorePoints.Add(restorePoint);
        return restorePoint;
    }

    public void RemoveRestorePoint(IRestorePoint restorePoint)
    {
        ArgumentNullException.ThrowIfNull(restorePoint);

        if (!_restorePoints.Remove(restorePoint))
            throw BackupException.RestorePointNotFound(restorePoint, this);
    }

    public override string ToString()
    {
        return $"Backup {Id}: {Name}";
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Backup);
    }

    public bool Equals(Backup? other)
    {
        return other is not null && Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}