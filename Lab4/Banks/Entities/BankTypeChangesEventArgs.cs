using Banks.Models.AccountTypes.Abstractions;

namespace Banks.Entities;

public class BankTypeChangesEventArgs : EventArgs
{
    public BankTypeChangesEventArgs(IAccountType accountType, string updateInfo)
    {
        AccountType = accountType;
        UpdateInfo = updateInfo;
    }

    public IAccountType AccountType { get; }
    public string UpdateInfo { get; }
}