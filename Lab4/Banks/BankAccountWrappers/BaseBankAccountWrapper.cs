using Banks.BankAccounts.Abstractions;
using Banks.Commands.Abstractions;
using Banks.Entities.Abstractions;
using Banks.Exceptions;
using Banks.Models;
using Banks.Models.Abstractions;
using Banks.Models.AccountTypes.Abstractions;

namespace Banks.BankAccountWrappers;

public class BaseBankAccountWrapper : IBankAccount
{
    private readonly IBankAccount _wrapped;

    public BaseBankAccountWrapper(IBankAccount wrapped)
    {
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

    public virtual void ExecuteCommand(ICommand command, ITransaction transaction)
    {
        // TODO Try-Catch clauses elsewhere
        try
        {
            command.Execute(this, transaction);
        }
        catch (TransactionException)
        {
            transaction.Cancel();
        }
    }

    public virtual MoneyAmount Withdraw(ITransaction transaction)
    {
        return _wrapped.Withdraw(transaction);
    }

    public virtual void Replenish(ITransaction transaction)
    {
        _wrapped.Replenish(transaction);
    }
}