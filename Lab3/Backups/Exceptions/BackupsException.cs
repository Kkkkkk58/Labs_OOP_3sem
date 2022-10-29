namespace Backups.Exceptions;

public class BackupsException : ApplicationException
{
    public BackupsException(string message)
        : base(message)
    {
    }
}