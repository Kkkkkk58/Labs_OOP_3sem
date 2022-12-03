using Banks.Entities.Abstractions;
using Banks.Models;
using Banks.Models.Abstractions;
using Banks.Tools.Abstractions;

namespace Banks.Entities;

public class BankBuilder : IBankBuilder, ISuspiciousLimitOperationsBankBuilder, IAccountFactoryBankBuilder, IClockBankBuilder, IFinishingBankBuilder
{
    private string? _name;
    private IAccountFactory? _accountFactory;
    private MoneyAmount? _limit;
    private IClock? _clock;

    public ISuspiciousLimitOperationsBankBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    public IClockBankBuilder SetAccountFactory(IAccountFactory accountFactory)
    {
        _accountFactory = accountFactory;
        return this;
    }

    public IAccountFactoryBankBuilder SetSuspiciousOperationsLimit(MoneyAmount limit)
    {
        _limit = limit;
        return this;
    }

    public IFinishingBankBuilder SetClock(IClock clock)
    {
        _clock = clock;
        return this;
    }

    public IBank Build()
    {
        ArgumentNullException.ThrowIfNull(_name);
        ArgumentNullException.ThrowIfNull(_accountFactory);
        ArgumentNullException.ThrowIfNull(_limit);
        ArgumentNullException.ThrowIfNull(_clock);

        return new Bank(_name, _accountFactory, _limit.Value, _clock);
    }
}