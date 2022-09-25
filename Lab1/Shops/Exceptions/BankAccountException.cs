using Shops.Entities;
using Shops.Models;

namespace Shops.Exceptions;

public class BankAccountException : ShopsException
{
    private BankAccountException(string message)
        : base(message)
    {
    }

    public static BankAccountException TransactionToSameAccountException(BankAccount account)
    {
        throw new BankAccountException($"Can't perform money transfer to the same account: {account.Id}");
    }

    public static BankAccountException NotEnoughMoneyException(BankAccount account, MoneyAmount moneyToSend)
    {
        throw new BankAccountException($"The bank account with id={account.Id} has {account.Balance} which is less than requested: {moneyToSend}");
    }
}