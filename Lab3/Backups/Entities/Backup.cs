using Backups.Exceptions;
using Backups.Models;

namespace Backups.Entities;

public class Backup
{
    private readonly List<RestorePoint> _restorePoints;

    public Backup(string name)
        : this(Guid.NewGuid(), name, new List<RestorePoint>())
    {
    }

    public Backup(Guid id, string name, IEnumerable<RestorePoint> restorePoints)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(restorePoints);

        Id = id;
        Name = name;
        _restorePoints = restorePoints.ToList();
    }

    public Guid Id { get; }
    public string Name { get; }
    public IReadOnlyList<RestorePoint> RestorePoints => _restorePoints.AsReadOnly();

    public RestorePoint AddRestorePoint(RestorePoint restorePoint)
    {
        ArgumentNullException.ThrowIfNull(restorePoint);
        if (_restorePoints.Contains(restorePoint))
            throw BackupException.RestorePointAlreadyExists(restorePoint, this);

        _restorePoints.Add(restorePoint);
        return restorePoint;
    }

    public void RemoveRestorePoint(RestorePoint restorePoint)
    {
        ArgumentNullException.ThrowIfNull(restorePoint);

        if (!_restorePoints.Remove(restorePoint))
            throw new NotImplementedException();
    }
}