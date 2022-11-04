using System.IO.Compression;
using Backups.Models.Abstractions;
using Backups.Tools.Archivers.Abstractions;

namespace Backups.Tools.Archivers;

public class ZipArchiver : IArchiver
{
    public string Extension => ".zip";

    public void Archive(IEnumerable<IBackupObject> backupObjects, Stream writingStream)
    {
        using var storageArchive = new ZipArchive(writingStream, ZipArchiveMode.Create);

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