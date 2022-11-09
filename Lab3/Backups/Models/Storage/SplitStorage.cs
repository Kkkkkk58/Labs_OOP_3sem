﻿using Backups.Models.Abstractions;
using Backups.Models.Storage.Abstractions;

namespace Backups.Models.Storage;

public class SplitStorage : IStorage
{
    private readonly IReadOnlyCollection<IStorage> _innerStorage;

    public SplitStorage(
        IRepository repository,
        IRepositoryAccessKey accessKey,
        IReadOnlyCollection<IStorage> innerStorage)
    {
        _innerStorage = innerStorage;
        AccessKey = accessKey;
        Repository = repository;
    }

    public IRepositoryAccessKey AccessKey { get; }
    public IRepository Repository { get; }
    public IEnumerable<IRepositoryObject> Objects => _innerStorage.SelectMany(storage => storage.Objects);
}