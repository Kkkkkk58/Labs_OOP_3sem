using Backups.Models.Abstractions;

namespace Backups.Models;

public class RestorePointBuilder : IRestorePointBuilder
{
    private DateTime? _restorePointDate;
    private IRestorePointVersion? _restorePointVersion;
    private IReadOnlyCollection<IObjectStorageRelation>? _objectStorageRelations;

    public IRestorePointBuilder SetDate(DateTime restorePointDate)
    {
        _restorePointDate = restorePointDate;
        return this;
    }

    public IRestorePointBuilder SetVersion(IRestorePointVersion restorePointVersion)
    {
        _restorePointVersion = restorePointVersion;
        return this;
    }

    public IRestorePointBuilder SetRelations(IReadOnlyCollection<IObjectStorageRelation> objectStorageRelations)
    {
        _objectStorageRelations = objectStorageRelations;
        return this;
    }

    public IRestorePoint Build()
    {
        ArgumentNullException.ThrowIfNull(_restorePointDate);
        ArgumentNullException.ThrowIfNull(_restorePointVersion);
        ArgumentNullException.ThrowIfNull(_objectStorageRelations);

        var restorePoint = new RestorePoint(_restorePointDate.Value, _restorePointVersion, _objectStorageRelations);
        Reset();

        return restorePoint;
    }

    private void Reset()
    {
        _objectStorageRelations = null;
        _restorePointDate = null;
        _restorePointVersion = null;
    }
}