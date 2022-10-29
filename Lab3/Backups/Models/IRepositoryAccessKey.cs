namespace Backups.Models;

public interface IRepositoryAccessKey
{
    string Value { get; }
    IRepositoryAccessKey CombineWithSeparator(IRepositoryAccessKey other);
    IRepositoryAccessKey CombineWithSeparator(string value);

    IRepositoryAccessKey CombineWithExtension(string extension);

    // TODO Combiner entity???
}