using Banks.Entities.Abstractions;

namespace Banks.Console.ViewModels;

public class BankViewModel
{
    private readonly INoTransactionalBank _bank;

    public BankViewModel(INoTransactionalBank bank)
    {
        _bank = bank;
    }

    public override string ToString()
    {
        return $"Bank {_bank.Name} [{_bank.Id}]";
    }
}