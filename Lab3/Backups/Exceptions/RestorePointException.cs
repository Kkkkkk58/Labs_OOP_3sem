namespace Backups.Exceptions;

public class RestorePointException : BackupsException
{
    private RestorePointException(string message)
        : base(message)
    {
    }

    public static RestorePointException ContainsRepeatingBackupObjects()
    {
        return new RestorePointException("Given collection of backup objects contains self-repeats");
    }
}