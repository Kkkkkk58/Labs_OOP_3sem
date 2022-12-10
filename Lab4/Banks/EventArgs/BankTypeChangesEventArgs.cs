using Banks.AccountTypes.Abstractions;

namespace Banks.EventArgs;

public class BankTypeChangesEventArgs : System.EventArgs
{
    public BankTypeChangesEventArgs(IAccountType accountType, string updateInfo)
    {
        AccountType = accountType;
        UpdateInfo = updateInfo;
    }

    public IAccountType AccountType { get; }
    public string UpdateInfo { get; }
}