using Banks.AccountTypes.Abstractions;
using Banks.BankAccounts.Abstractions;
using Banks.Commands.Abstractions;
using Banks.Entities.Abstractions;
using Banks.Exceptions;
using Banks.Models;
using Banks.Models.Abstractions;
using Banks.Transactions.Abstractions;

namespace Banks.BankAccounts.Wrappers;

public class BaseBankAccountWrapper : IBankAccount
{
    private readonly IBankAccount _wrapped;

    public BaseBankAccountWrapper(IBankAccount wrapped)
    {
        ArgumentNullException.ThrowIfNull(wrapped);

        _wrapped = wrapped;
    }

    public Guid Id => _wrapped.Id;
    public IAccountType Type => _wrapped.Type;
    public ICustomer Holder => _wrapped.Holder;
    public MoneyAmount Balance => _wrapped.Balance;
    public MoneyAmount Debt => _wrapped.Debt;
    public DateTime CreationDate => _wrapped.CreationDate;
    public MoneyAmount InitialBalance => _wrapped.InitialBalance;
    public bool IsSuspicious => _wrapped.IsSuspicious;
    public IReadOnlyCollection<IOperationInformation> OperationHistory => _wrapped.OperationHistory;

    public void ExecuteCommand(ICommand command, ITransaction transaction)
    {
        ArgumentNullException.ThrowIfNull(command);
        ArgumentNullException.ThrowIfNull(transaction);

        try
        {
            command.Execute(this, transaction);
        }
        catch (TransactionException)
        {
            transaction.Cancel();
            throw;
        }
    }

    public virtual MoneyAmount Withdraw(ITransaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction);

        return _wrapped.Withdraw(transaction);
    }

    public virtual void Replenish(ITransaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction);

        _wrapped.Replenish(transaction);
    }
}