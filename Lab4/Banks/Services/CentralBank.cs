using Banks.BankAccounts.Abstractions;
using Banks.Commands;
using Banks.Entities.Abstractions;
using Banks.Models;
using Banks.Models.Abstractions;
using Banks.Services.Abstractions;
using Banks.Tools.Abstractions;

namespace Banks.Services;

public class CentralBank : ICentralBank
{
    private readonly List<IBank> _banks;
    private readonly IClock _clock;
    private readonly List<ITransaction> _transactions;

    public CentralBank(IClock clock)
    {
        _banks = new List<IBank>();
        _transactions = new List<ITransaction>();
        _clock = clock;
    }

    public IReadOnlyCollection<INoTransactionalBank> Banks => _banks;

    public IReadOnlyCollection<IOperationInformation> Operations => _transactions
        .Select(t => t.Information)
        .ToList()
        .AsReadOnly();

    public INoTransactionalBank RegisterBank(IBank bank)
    {
        if (_banks.Contains(bank))
            throw new NotImplementedException();
        _banks.Add(bank);
        return bank;
    }

    public void CancelTransaction(Guid transactionId)
    {
        ITransaction transaction =
            _transactions.Single(transaction => transaction.Information.Id.Equals(transactionId));

        transaction.Cancel();
    }

    // TODO Common transaction creation
    public void Withdraw(Guid accountId, MoneyAmount moneyAmount)
    {
        ICommandExecutingBankAccount commandExecutingBankAccount = GetCommandExecutingBankAccount(accountId);
        var operationInformation = new OperationInformation(commandExecutingBankAccount, moneyAmount, _clock.Now, "\\");
        var transaction = new Transaction(operationInformation, new WithdrawalCommand());
        _transactions.Add(transaction);

        transaction.Perform();
        transaction.Information.SetCompletionTime(_clock.Now);
        transaction.ChangeState(new SuccessfulTransactionState(transaction));
    }

    public void Replenish(Guid accountId, MoneyAmount moneyAmount)
    {
        ICommandExecutingBankAccount commandExecutingBankAccount = GetCommandExecutingBankAccount(accountId);
        var operationInformation = new OperationInformation(commandExecutingBankAccount, moneyAmount, _clock.Now, "\\");
        var transaction = new Transaction(operationInformation, new ReplenishmentCommand());
        _transactions.Add(transaction);

        transaction.Perform();
        transaction.Information.SetCompletionTime(_clock.Now);
        transaction.ChangeState(new SuccessfulTransactionState(transaction));
    }

    public void Transfer(Guid fromAccountId, Guid toAccountId, MoneyAmount moneyAmount)
    {
        ICommandExecutingBankAccount from = GetCommandExecutingBankAccount(fromAccountId);
        ICommandExecutingBankAccount to = GetCommandExecutingBankAccount(toAccountId);

        var operationInformation = new OperationInformation(from, moneyAmount, _clock.Now, "\\");
        var transaction = new Transaction(operationInformation, new TransferCommand(to));
        _transactions.Add(transaction);

        transaction.Perform();
        transaction.Information.SetCompletionTime(_clock.Now);
        transaction.ChangeState(new SuccessfulTransactionState(transaction));
    }

    private ICommandExecutingBankAccount GetCommandExecutingBankAccount(Guid accountId)
    {
        foreach (IBank bank in _banks)
        {
            IUnchangeableBankAccount? account = bank.FindAccount(accountId);
            if (account is null)
                continue;

            return bank.GetExecutingAccount(accountId);
        }

        throw new NotImplementedException();
    }
}