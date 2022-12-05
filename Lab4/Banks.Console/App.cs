using Banks.Console.Handlers.Abstractions;
using Banks.Console.ReadWrite.Abstractions;
using Banks.Exceptions;
using Banks.Models.Abstractions;
using Banks.Services.Abstractions;
using Banks.Tools.Abstractions;

namespace Banks.Console;

public class App
{
    private readonly AppContext _context;
    private readonly IHandler _baseHandler;

    public App(
        IFastForwardingClock clock,
        ICentralBank centralBank,
        IAccountFactory accountFactory,
        IReader reader,
        IWriter writer)
    {
        _context = new AppContext(centralBank, clock, accountFactory, reader, writer);
        _baseHandler = new CommandTreeConfigurator(_context, clock)
            .Configure();
    }

    public void Run()
    {
        try
        {
            while (true)
            {
                try
                {
                    string[] command = _context.Reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    _baseHandler.Handle(command);
                }
                catch (BanksException e)
                {
                    _context.Writer.WriteLine(e);
                }
            }
        }
        catch (Exception e)
        {
            throw new ApplicationException("An error occurred during the execution. Shutting down the app...", e);
        }
    }
}