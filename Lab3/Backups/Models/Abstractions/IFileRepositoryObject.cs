namespace Backups.Models.Abstractions;

public interface IFileRepositoryObject : IRepositoryObject
{
    Stream Stream { get; }
}