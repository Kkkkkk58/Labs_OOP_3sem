using System.IO.Compression;
using Backups.Models.Abstractions;

namespace Backups.Models;

public class ZipArchiver : IArchiver
{
    public string Extension => ".zip";

    public void Archive(IEnumerable<IBackupObject> backupObjects, Stream writingStream)
    {
        using var storageArchive = new ZipArchive(writingStream, ZipArchiveMode.Create, leaveOpen: true);

        foreach (IBackupObject backupObject in backupObjects)
        {
            IReadOnlyList<RepositoryObject> objectContents = backupObject.GetContents();
            foreach (RepositoryObject objectContent in objectContents)
            {
                WriteToArchive(storageArchive, objectContent);
            }
        }
    }

    private static void WriteToArchive(ZipArchive storageArchive, RepositoryObject repositoryObject)
    {
        using Stream backupObjectContent = repositoryObject.Stream;
        ZipArchiveEntry zipArchiveEntry = storageArchive.CreateEntry(repositoryObject.AccessKey.Value);
        using Stream arcStream = zipArchiveEntry.Open();
        backupObjectContent.CopyTo(arcStream);
    }
}