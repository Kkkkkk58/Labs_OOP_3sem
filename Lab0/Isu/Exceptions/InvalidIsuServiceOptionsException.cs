namespace Isu.Exceptions;

public class InvalidIsuServiceOptionsException : IsuException
{
    private InvalidIsuServiceOptionsException(string message)
        : base(message)
    {
    }

    public static InvalidIsuServiceOptionsException NegativeGroupLimit(int groupLimit)
    {
        return new InvalidIsuServiceOptionsException($"Group limit must be non-negative but was {groupLimit}");
    }
}