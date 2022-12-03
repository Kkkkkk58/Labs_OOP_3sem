using Banks.Models.Abstractions;
using Banks.Services.Abstractions;
using Banks.Tools.Abstractions;

namespace Banks.Console;

public struct AppContext
{
    public AppContext(ICentralBank centralBank, IClock clock, IAccountFactory accountFactory)
    {
        CentralBank = centralBank;
        Clock = clock;
        AccountFactory = accountFactory;
    }

    public ICentralBank CentralBank { get; }
    public IClock Clock { get; }
    public IAccountFactory AccountFactory { get; }
}