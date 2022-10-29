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
            IReadOnlyList<BackupObjectContent> objectContents = backupObject.GetContents();
            foreach (BackupObjectContent objectContent in objectContents)
            {
                WriteToArchive(storageArchive, objectContent);
            }
        }
    }

    private static void WriteToArchive(ZipArchive storageArchive, BackupObjectContent objectContent)
    {
        using Stream backupObjectContent = objectContent.Stream;
        ZipArchiveEntry zipArchiveEntry = storageArchive.CreateEntry(objectContent.AccessKey.Value);
        using Stream arcStream = zipArchiveEntry.Open();
        backupObjectContent.CopyTo(arcStream);
    }
}