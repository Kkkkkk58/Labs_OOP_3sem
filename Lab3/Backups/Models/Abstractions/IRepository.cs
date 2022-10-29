namespace Backups.Models.Abstractions;

public interface IRepository
{
    bool Contains(RepositoryAccessKey accessKey);
    Stream GetData(RepositoryAccessKey accessKey);
    void SaveData(RepositoryAccessKey accessKey, Stream content);
}