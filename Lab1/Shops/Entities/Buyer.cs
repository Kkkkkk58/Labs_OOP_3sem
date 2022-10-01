using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class Buyer : IEquatable<Buyer>
{
    private readonly BankAccount _bankAccount;

    public Buyer(string name, BankAccount bankAccount)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw BuyerException.EmptyNameException();

        Id = Guid.NewGuid();
        Name = name;
        _bankAccount = bankAccount;
    }

    public Guid Id { get; }
    public string Name { get; }
    public MoneyAmount Balance => _bankAccount.Balance;

    public void SpendMoneyInShop(MoneyAmount expenditure, BankAccount shopsAccount)
    {
        _bankAccount.SendMoney(shopsAccount, expenditure);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Buyer);
    }

    public bool Equals(Buyer? other)
    {
        return other is not null
               && Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}