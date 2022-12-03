using System.Globalization;
using Banks.Models;
using Banks.Models.Abstractions;
using Banks.Services;
using Banks.Services.Abstractions;
using Banks.Tools;
using Banks.Tools.Abstractions;

namespace Banks.Test;

public class BanksTests
{
    private readonly ICentralBank _centralBank;
    private readonly IClock _clock;
    private readonly IAccountFactory _accountFactory;

    public BanksTests()
    {
        _clock = new BasicFastForwardingClock(DateTime.Now);
        _centralBank = new CentralBank(_clock);
        _accountFactory = new BankAccountFactory(_clock, new GregorianCalendar());
    }
}