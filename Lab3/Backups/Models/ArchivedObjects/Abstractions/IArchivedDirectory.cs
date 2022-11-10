namespace Backups.Models.ArchivedObjects.Abstractions;

public interface IArchivedDirectory : IArchivedObject
{
    IEnumerable<IArchivedObject> Children { get; }
}