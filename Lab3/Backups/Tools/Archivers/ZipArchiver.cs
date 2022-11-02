using System.IO.Compression;
using Backups.Archivers.Abstractions;
using Backups.Models.Abstractions;

namespace Backups.Models.Archivers;

public class ZipArchiver : IArchiver
{
    public string Extension => ".zip";

    public void Archive(IEnumerable<IBackupObject> backupObjects, Stream writingStream)
    {
        using var storageArchive = new ZipArchive(writingStream, ZipArchiveMode.Create, leaveOpen: true);

        foreach (IBackupObject backupObject in backupObjects)
        {
            IReadOnlyList<IRepositoryObject> objectContents = backupObject.GetContents();
            foreach (IRepositoryObject objectContent in objectContents)
            {
                WriteToArchive(storageArchive, objectContent);
            }
        }
    }

    private static void WriteToArchive(ZipArchive storageArchive, IRepositoryObject repositoryObject)
    {
        using Stream backupObjectContent = repositoryObject.Stream;
        ZipArchiveEntry zipArchiveEntry = storageArchive.CreateEntry(repositoryObject.AccessKey.Value);
        using Stream arcStream = zipArchiveEntry.Open();
        backupObjectContent.CopyTo(arcStream);
    }
}