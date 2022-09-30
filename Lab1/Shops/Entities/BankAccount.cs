using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class BankAccount : IEquatable<BankAccount>
{
    public BankAccount(MoneyAmount balance)
    {
        Id = Guid.NewGuid();
        Balance = balance;
    }

    public Guid Id { get; }
    public MoneyAmount Balance { get; private set; }

    public MoneyAmount SendMoney(BankAccount receiver, MoneyAmount moneyToSend)
    {
        if (Equals(receiver))
            throw BankAccountException.TransactionToSameAccountException(receiver);

        if (Balance < moneyToSend)
            throw BankAccountException.NotEnoughMoneyException(this, moneyToSend);

        receiver.Balance += moneyToSend;
        Balance -= moneyToSend;

        return moneyToSend;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as BankAccount);
    }

    public bool Equals(BankAccount? other)
    {
        return other is not null && Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}