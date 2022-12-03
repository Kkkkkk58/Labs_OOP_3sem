namespace Banks.Console.Chains;

public interface ICompositeHandler : IHandler
{
    ICompositeHandler AddSubHandler(IHandler handler);
}