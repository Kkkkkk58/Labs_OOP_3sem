namespace Backups.Models.Abstractions;

public interface IRepositoryObject
{
    IRepositoryAccessKey AccessKey { get; }
    Stream Stream { get; }
}