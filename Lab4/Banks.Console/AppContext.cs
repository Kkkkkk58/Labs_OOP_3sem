using Banks.Console.ReadWrite.Abstractions;
using Banks.Models.Abstractions;
using Banks.Services.Abstractions;
using Banks.Tools.Abstractions;

namespace Banks.Console;

public struct AppContext
{
    public AppContext(
        ICentralBank centralBank,
        IClock clock,
        IAccountFactory accountFactory,
        IReader reader,
        IWriter writer)
    {
        CentralBank = centralBank;
        Clock = clock;
        AccountFactory = accountFactory;
        Reader = reader;
        Writer = writer;
    }

    public ICentralBank CentralBank { get; }
    public IClock Clock { get; }
    public IAccountFactory AccountFactory { get; }
    public IReader Reader { get; }
    public IWriter Writer { get; }
}