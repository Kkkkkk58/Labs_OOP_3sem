﻿using System.IO.Compression;
using Backups.Models.Abstractions;
using Backups.Models.ArchivedObjects.Abstractions;
using Backups.Models.Repository.Abstractions;
using Backups.Models.RepositoryObjects.Abstractions;
using Backups.Models.Storage;
using Backups.Models.Storage.Abstractions;
using Backups.Models.Visitors;
using Backups.Tools.Archiver.Abstractions;

namespace Backups.Tools.Archiver;

public class ZipArchiver : IArchiver
{
    private const string Extension = "zip";

    public IStorage Archive(IEnumerable<IRepositoryObject> repositoryObjects, IRepository repository, IRepositoryAccessKey partialArchiveKey)
    {
        ArgumentNullException.ThrowIfNull(repositoryObjects);
        ArgumentNullException.ThrowIfNull(repository);
        ArgumentNullException.ThrowIfNull(partialArchiveKey);

        IRepositoryAccessKey archiveKey = partialArchiveKey.ApplyExtension(Extension);

        using Stream stream = repository.OpenWrite(archiveKey);
        using var storageArchive = new ZipArchive(stream, ZipArchiveMode.Create);
        var visitor = new ZipArchiveVisitor(storageArchive);

        foreach (IRepositoryObject repositoryObject in repositoryObjects)
        {
            repositoryObject.Accept(visitor);
        }

        IReadOnlyCollection<IArchivedObject> archivedObjects = visitor.GetArchivedObjects();

        return new ZipStorage(repository, archiveKey, archivedObjects);
    }
}