﻿using Backups.Models.ArchivedObjects.Abstractions;
using Backups.Models.RepositoryObjects.Abstractions;

namespace Backups.Models.Visitors.Abstractions;

public interface IRepositoryObjectVisitor
{
    void Visit(IFileRepositoryObject fileRepositoryObject);
    void Visit(IDirectoryRepositoryObject directoryRepositoryObject);

    IReadOnlyCollection<IArchivedObject> GetArchivedObjects();
}