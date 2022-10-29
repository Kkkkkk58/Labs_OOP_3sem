using Backups.Models.Abstractions;

namespace Backups.Models;

public class CompositeBackupObject : IBackupObject
{
    private readonly List<IBackupObject> _childBackupObjects;

    public CompositeBackupObject(IRepository sourceRepository, RepositoryAccessKey accessKey)
    {
        AccessKey = accessKey;
        SourceRepository = sourceRepository;
        _childBackupObjects = new List<IBackupObject>();
    }

    public RepositoryAccessKey AccessKey { get; }
    public IRepository SourceRepository { get; }

    public IReadOnlyList<BackupObjectContent> GetContents()
    {
        return _childBackupObjects.SelectMany(cb => cb.GetContents()).ToList().AsReadOnly();
    }

    public IBackupObject AddBackupObject(IBackupObject backupObject)
    {
        if (_childBackupObjects.Contains(backupObject))
            throw new NotImplementedException();
        _childBackupObjects.Add(backupObject);
        return backupObject;
    }

    public void RemoveBackupObject(IBackupObject backupObject)
    {
        if (!_childBackupObjects.Remove(backupObject))
            throw new NotImplementedException();
    }
}