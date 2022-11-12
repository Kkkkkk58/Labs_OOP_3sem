namespace Backups.Models.ArchivedObjects.Abstractions;

public interface IArchivedDirectory : IArchivedObject
{
    IReadOnlyCollection<IArchivedObject> Children { get; }
}