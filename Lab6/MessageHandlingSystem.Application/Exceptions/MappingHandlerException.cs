namespace MessageHandlingSystem.Application.Exceptions;

public class MappingHandlerException : ApplicationException
{
    private MappingHandlerException(string message)
        : base(message)
    {
    }

    public static MappingHandlerException MappingIsUndefined()
    {
        return new MappingHandlerException($"Suitable mapping handler not found");
    }
}