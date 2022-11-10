namespace Backups.Models.ArchivedObjects.Abstractions;

public interface IArchivedFolder : IArchivedObject
{
    IEnumerable<IArchivedObject> Children { get; }
}