namespace Isu.Exceptions;

public class IsuException : ApplicationException
{
    public IsuException(string message)
        : base(message)
    {
    }
}