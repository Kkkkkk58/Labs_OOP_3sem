using Banks.Models;
using Banks.Models.Abstractions;
using Banks.Tools.Abstractions;

namespace Banks.Entities.Abstractions;

public interface IBankBuilder
{
    ISuspiciousLimitOperationsBankBuilder SetName(string name);
}

public interface ISuspiciousLimitOperationsBankBuilder
{
    IAccountFactoryBankBuilder SetSuspiciousOperationsLimit(MoneyAmount limit);
}

public interface IAccountFactoryBankBuilder
{
    IClockBankBuilder SetAccountFactory(IAccountFactory accountFactory);
}

public interface IClockBankBuilder
{
    IFinishingBankBuilder SetClock(IClock clock);
}

public interface IFinishingBankBuilder
{
    IBank Build();
}