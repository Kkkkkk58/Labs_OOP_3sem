namespace Backups.Models.Abstractions;

public interface IRestorePointVersion
{
    IRestorePointVersion GetNext();
}