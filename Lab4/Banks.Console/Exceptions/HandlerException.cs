namespace Banks.Console.Exceptions;

public class HandlerException : BanksConsoleException
{
    private HandlerException(string message)
        : base(message)
    {
    }

    public static HandlerException InvalidRequestType(string handledRequest, string actualRequest)
    {
        return new HandlerException($"Handler {handledRequest} doesn't handle {actualRequest} requests");
    }

    public static HandlerException InvalidRequestParametersLength(int argsLength)
    {
        return new HandlerException($"Invalid number of request parameters: {argsLength}");
    }
}