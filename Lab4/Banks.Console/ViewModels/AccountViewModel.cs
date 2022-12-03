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
        return $"Bank account [{_account.Id}]\nHolder: [{_account.Holder.Id}]\nBalance: {_account.Balance}\nDebt: {_account.Debt}\nCreation date: {_account.CreationDate}\n";
    }
}