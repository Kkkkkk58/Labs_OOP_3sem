namespace Banks.Console.Handlers.Abstractions;

public interface ICompositeHandler : IHandler
{
    ICompositeHandler AddSubHandler(IHandler handler);
}