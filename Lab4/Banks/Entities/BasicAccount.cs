using Banks.BankAccounts.Abstractions;
using Banks.Commands.Abstractions;
using Banks.Entities.Abstractions;
using Banks.Models;
using Banks.Models.Abstractions;
using Banks.Models.AccountTypes.Abstractions;
using Banks.Tools.Abstractions;

namespace Banks.Entities;

public class BasicAccount : IBankAccount
{
    private readonly List<IOperationInformation> _operations;

    public BasicAccount(IAccountType type, ICustomer holder, MoneyAmount balance, IClock clock, List<IOperationInformation>? operationHistory = null)
    {
        Id = Guid.NewGuid();
        Type = type;
        Holder = holder;
        InitialBalance = Balance = balance;
        Debt = new MoneyAmount(0);
        CreationDate = clock.Now;
        _operations = operationHistory ?? new List<IOperationInformation>();
    }

    public Guid Id { get; }
    public IAccountType Type { get; }
    public ICustomer Holder { get; }
    public MoneyAmount Balance { get; private set; }
    public MoneyAmount Debt { get; private set; }
    public DateTime CreationDate { get; }
    public MoneyAmount InitialBalance { get; }
    public bool IsSuspicious => !Holder.IsVerified;
    public IReadOnlyCollection<IOperationInformation> OperationHistory => _operations;

    public void ExecuteCommand(ICommand command, ITransaction transaction)
    {
        command.Execute(this, transaction);
    }

    public MoneyAmount Withdraw(ITransaction transaction)
    {
        MoneyAmount moneyAmount = transaction.Information.OperatedAmount;

        if (Balance < moneyAmount)
        {
            Debt = moneyAmount - Balance;
            Balance = new MoneyAmount(0, Balance.CurrencySign);
        }
        else
        {
            Balance -= moneyAmount;
        }

        _operations.Add(transaction.Information);
        return moneyAmount;
    }

    public void Replenish(ITransaction transaction)
    {
        MoneyAmount moneyAmount = transaction.Information.OperatedAmount;

        if (Debt.Value == 0)
        {
            Balance += moneyAmount;
        }
        else
        {
            if (Debt <= moneyAmount)
            {
                Balance += moneyAmount - Debt;
                Debt = new MoneyAmount(0, Debt.CurrencySign);
            }
            else
            {
                Debt -= moneyAmount;
            }
        }

        _operations.Add(transaction.Information);
    }
}