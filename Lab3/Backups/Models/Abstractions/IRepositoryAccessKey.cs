namespace Backups.Models.Abstractions;

public interface IRepositoryAccessKey
{
    string Value { get; }
    IRepositoryAccessKey Combine(IRepositoryAccessKey other);
    IRepositoryAccessKey Combine(string value);

    IRepositoryAccessKey ApplyExtension(string extension);
}