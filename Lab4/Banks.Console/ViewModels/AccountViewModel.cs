using Banks.BankAccounts.Abstractions;

namespace Banks.Console.ViewModels;

public class AccountViewModel
{
    private readonly IUnchangeableBankAccount _account;

    public AccountViewModel(IUnchangeableBankAccount account)
    {
        _account = account;
    }

    public override string ToString()
    {
        return
            $"Bank account [{_account.Id}]\n" +
            $"Holder: [{_account.Holder.Id}]\n" +
            $"Balance: {_account.Balance}\n" +
            $"Debt: {_account.Debt}\n" +
            $"Creation date: {_account.CreationDate}";
    }
}