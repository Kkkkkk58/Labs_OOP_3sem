﻿using Banks.BankAccounts.Abstractions;
using Banks.Commands;
using Banks.Entities.Abstractions;
using Banks.Exceptions;
using Banks.Models;
using Banks.Models.Abstractions;
using Banks.Services.Abstractions;
using Banks.Tools.Abstractions;
using Banks.Transactions;
using Banks.Transactions.Abstractions;

namespace Banks.Services;

public class CentralBank : ICentralBank
{
    private readonly List<IBank> _banks;
    private readonly IClock _clock;
    private readonly List<ITransaction> _transactions;

    public CentralBank(IClock clock)
    {
        ArgumentNullException.ThrowIfNull(clock);

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
        ArgumentNullException.ThrowIfNull(bank);
        if (_banks.Contains(bank))
            throw CentralBankException.BankAlreadyExists(bank.Id);

        _banks.Add(bank);
        return bank;
    }

    public void CancelTransaction(Guid transactionId)
    {
        ITransaction transaction = _transactions
            .Single(transaction => transaction.Information.Id.Equals(transactionId));

        transaction.Cancel();
    }

    public IOperationInformation Withdraw(Guid accountId, MoneyAmount moneyAmount)
    {
        ICommandExecutingBankAccount commandExecutingBankAccount = GetCommandExecutingBankAccount(accountId);
        var operationInformation = new OperationInformation(commandExecutingBankAccount, moneyAmount, _clock.Now);
        var transaction = new Transaction(operationInformation, new WithdrawalCommand());

        return PerformTransaction(transaction);
    }

    public IOperationInformation Replenish(Guid accountId, MoneyAmount moneyAmount)
    {
        ICommandExecutingBankAccount commandExecutingBankAccount = GetCommandExecutingBankAccount(accountId);
        var operationInformation = new OperationInformation(commandExecutingBankAccount, moneyAmount, _clock.Now);
        var transaction = new Transaction(operationInformation, new ReplenishmentCommand());

        return PerformTransaction(transaction);
    }

    public IOperationInformation Transfer(Guid fromAccountId, Guid toAccountId, MoneyAmount moneyAmount)
    {
        ICommandExecutingBankAccount from = GetCommandExecutingBankAccount(fromAccountId);
        ICommandExecutingBankAccount to = GetCommandExecutingBankAccount(toAccountId);

        var operationInformation = new OperationInformation(from, moneyAmount, _clock.Now);
        var transaction = new Transaction(operationInformation, new TransferCommand(to));

        return PerformTransaction(transaction);
    }

    private IOperationInformation PerformTransaction(ITransaction transaction)
    {
        _transactions.Add(transaction);

        transaction.Perform();
        transaction.Information.SetCompletionTime(_clock.Now);
        transaction.ChangeState(new SuccessfulTransactionState(transaction));

        return transaction.Information;
    }

    private ICommandExecutingBankAccount GetCommandExecutingBankAccount(Guid accountId)
    {
        return _banks
            .Single(bank => bank.FindAccount(accountId) is not null)
            .GetExecutingAccount(accountId);
    }
}