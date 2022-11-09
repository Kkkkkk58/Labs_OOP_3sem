namespace Backups.Models.Abstractions;

public interface IArchivedFolder : IArchivedObject
{
    IEnumerable<IArchivedObject> Children { get; }
}