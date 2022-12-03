using Banks.Console.Chains;
using Banks.Models.Abstractions;
using Banks.Services.Abstractions;
using Banks.Tools.Abstractions;

namespace Banks.Console;

public class App
{
    private readonly IFastForwardingClock _clock;
    private readonly ICentralBank _centralBank;
    private readonly IAccountFactory _accountFactory;
    private readonly IHandler _baseHandler;

    // TODO IReader reader, IWriter writer
    public App(IFastForwardingClock clock, ICentralBank centralBank, IAccountFactory accountFactory)
    {
        _clock = clock;
        _centralBank = centralBank;
        _accountFactory = accountFactory;
        _baseHandler = new CommandTreeConfigurator(new AppContext(_centralBank, _clock, _accountFactory), _clock)
            .Configure();
    }

    public void Run()
    {
        try
        {
            while (true)
            {
                string[] command = System.Console.ReadLine()?.Split(" ", StringSplitOptions.RemoveEmptyEntries) ??
                                   throw new NotImplementedException();
                _baseHandler.Handle(command);
            }
        }
        catch (Exception e)
        {
            throw new ApplicationException("An error occurred during the execution. Shutting down the app...", e);
        }
    }

    private IHandler ConfigureHandlers()
    {
        throw new NotImplementedException();
    }
}