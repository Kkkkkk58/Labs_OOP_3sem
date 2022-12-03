using Banks.Console.Chains;

namespace Banks.Console.Controllers;

public class MainHandler : CompositeHandler
{
    public MainHandler()
        : base("banks")
    {
    }
}