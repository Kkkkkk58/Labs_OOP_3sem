using System.Globalization;
using Banks.AccountTypes.Abstractions;
using Banks.BankAccounts.Abstractions;
using Banks.Entities;
using Banks.Entities.Abstractions;
using Banks.Exceptions;
using Banks.Models;
using Banks.Models.Abstractions;
using Banks.Services;
using Banks.Services.Abstractions;
using Banks.Tools;
using Banks.Tools.Abstractions;
using Xunit;

namespace Banks.Test;

public class BanksTests
{
    private readonly ICentralBank _centralBank;
    private readonly IFastForwardingClock _clock;
    private readonly IAccountFactory _accountFactory;
    private readonly Calendar _calendar;

    public BanksTests()
    {
        _clock = new BasicFastForwardingClock(DateTime.Now);
        _centralBank = new CentralBank(_clock);
        _calendar = new GregorianCalendar();
        _accountFactory = new BankAccountFactory(_clock, _calendar);
    }

    [Fact]
    public void RegisterClientWithAddressAndPassportData_ClientIsVerified()
    {
        ICustomer client = GetVerifiedCustomer();

        Assert.True(client.IsVerified);
    }

    [Fact]
    public void RegisterClientWithoutAddressAndPassportData_ClientIsNotVerified()
    {
        ICustomer client = GetNotVerifiedCustomer();

        Assert.False(client.IsVerified);
    }

    [Fact]
    public void ReplenishMoney_BalanceIncreases()
    {
        var suspiciousOperationsLimit = new MoneyAmount(10);
        INoTransactionalBank bank = GetBank(suspiciousOperationsLimit);
        ICustomer client = GetVerifiedCustomer();
        bank.RegisterCustomer(client);
        IDebitAccountType accountType = bank.AccountTypeManager.CreateDebitAccountType(12, TimeSpan.Zero);
        var initialBalance = new MoneyAmount(15);
        IUnchangeableBankAccount account = bank.CreateDebitAccount(accountType, client, initialBalance);

        var replenishment = new MoneyAmount(12);
        _centralBank.Replenish(account.Id, replenishment);
        Assert.Equal(initialBalance + replenishment, account.Balance);
    }

    [Fact]
    public void WithdrawMoney_BalanceDecreases()
    {
        var suspiciousOperationsLimit = new MoneyAmount(10);
        INoTransactionalBank bank = GetBank(suspiciousOperationsLimit);
        ICustomer client = GetVerifiedCustomer();
        bank.RegisterCustomer(client);
        IDebitAccountType accountType = bank.AccountTypeManager.CreateDebitAccountType(12, TimeSpan.Zero);
        var initialBalance = new MoneyAmount(15);
        IUnchangeableBankAccount account = bank.CreateDebitAccount(accountType, client, initialBalance);

        var withdrawal = new MoneyAmount(12);
        _centralBank.Withdraw(account.Id, withdrawal);
        Assert.Equal(initialBalance - withdrawal, account.Balance);
    }

    [Fact]
    public void TransferMoney_ReceiverGetsMoneySenderGivesMoney()
    {
        var suspiciousOperationsLimit = new MoneyAmount(10);
        INoTransactionalBank bank = GetBank(suspiciousOperationsLimit);
        ICustomer client = GetVerifiedCustomer();
        bank.RegisterCustomer(client);
        IDebitAccountType accountType = bank.AccountTypeManager.CreateDebitAccountType(12, TimeSpan.Zero);
        var initialBalance = new MoneyAmount(15);
        IUnchangeableBankAccount sender = bank.CreateDebitAccount(accountType, client, initialBalance);
        IUnchangeableBankAccount receiver = bank.CreateDebitAccount(accountType, client, initialBalance);

        var transfer = new MoneyAmount(12);
        _centralBank.Transfer(sender.Id, receiver.Id, transfer);
        Assert.Equal(initialBalance - transfer, sender.Balance);
        Assert.Equal(initialBalance + transfer, receiver.Balance);
    }

    [Fact]
    public void PerformOperationBeyondSuspiciousOperationLimit_ThrowsException()
    {
        var suspiciousOperationsLimit = new MoneyAmount(10);
        INoTransactionalBank bank = GetBank(suspiciousOperationsLimit);
        ICustomer client = GetNotVerifiedCustomer();
        bank.RegisterCustomer(client);
        IDebitAccountType accountType = bank.AccountTypeManager.CreateDebitAccountType(12, TimeSpan.Zero);
        IUnchangeableBankAccount account = bank.CreateDebitAccount(accountType, client);

        Assert.Throws<TransactionException>(() => _centralBank.Replenish(account.Id, new MoneyAmount(15)));
    }

    [Fact]
    public void ReachDebtWithNonNegativeBalanceAccount_ThrowsException()
    {
        var suspiciousOperationsLimit = new MoneyAmount(100000);
        INoTransactionalBank bank = GetBank(suspiciousOperationsLimit);
        ICustomer client = GetNotVerifiedCustomer();
        bank.RegisterCustomer(client);
        IDebitAccountType accountType = bank.AccountTypeManager.CreateDebitAccountType(10, TimeSpan.Zero);
        IUnchangeableBankAccount account = bank.CreateDebitAccount(accountType, client);

        Assert.Throws<TransactionException>(() => _centralBank.Withdraw(account.Id, new MoneyAmount(1)));
    }

    [Fact]
    public void ReachDebtWithNegativeBalanceAvailableBankAccount_HasDebt()
    {
        var suspiciousOperationsLimit = new MoneyAmount(100000);
        INoTransactionalBank bank = GetBank(suspiciousOperationsLimit);
        ICustomer client = GetNotVerifiedCustomer();
        bank.RegisterCustomer(client);
        ICreditAccountType accountType = bank.AccountTypeManager.CreateCreditAccountType(new MoneyAmount(15), new MoneyAmount(0));
        IUnchangeableBankAccount account = bank.CreateCreditAccount(accountType, client);

        var withdrawalAmount = new MoneyAmount(10);
        _centralBank.Withdraw(account.Id, withdrawalAmount);
        Assert.Equal(account.Balance, new MoneyAmount(0));
        Assert.Equal(account.Debt, withdrawalAmount);
    }

    [Fact]
    public void ReachDebtLimitWithCreditAccount_ThrowsException()
    {
        var suspiciousOperationsLimit = new MoneyAmount(100000);
        INoTransactionalBank bank = GetBank(suspiciousOperationsLimit);
        ICustomer client = GetNotVerifiedCustomer();
        bank.RegisterCustomer(client);
        ICreditAccountType accountType = bank.AccountTypeManager.CreateCreditAccountType(new MoneyAmount(15), new MoneyAmount(0));
        IUnchangeableBankAccount account = bank.CreateCreditAccount(accountType, client);

        Assert.Throws<TransactionException>(() => _centralBank.Withdraw(account.Id, new MoneyAmount(20)));
    }

    [Fact]
    public void CreditAccountWithDebt_AccountCharged()
    {
        var suspiciousOperationsLimit = new MoneyAmount(100000);
        INoTransactionalBank bank = GetBank(suspiciousOperationsLimit);
        ICustomer client = GetNotVerifiedCustomer();
        bank.RegisterCustomer(client);
        var charge = new MoneyAmount(10);
        ICreditAccountType accountType = bank.AccountTypeManager.CreateCreditAccountType(new MoneyAmount(10000), charge);
        IUnchangeableBankAccount account = bank.CreateCreditAccount(accountType, client);

        var withdrawal = new MoneyAmount(1);
        _centralBank.Withdraw(account.Id, withdrawal);
        _centralBank.Withdraw(account.Id, withdrawal);
        Assert.Equal((2 * withdrawal) + charge, account.Debt);
    }

    [Fact]
    public void DepositAccountWithdrawalBeforeLimit_ThrowsException()
    {
        var suspiciousOperationsLimit = new MoneyAmount(100000);
        INoTransactionalBank bank = GetBank(suspiciousOperationsLimit);
        ICustomer client = GetNotVerifiedCustomer();
        bank.RegisterCustomer(client);
        var depositTerm = TimeSpan.FromDays(90);
        InterestOnBalancePolicy interestOnBalancePolicy = GetInterestOnBalancePolicy();
        IDepositAccountType accountType = bank.AccountTypeManager.CreateDepositAccountType(depositTerm, interestOnBalancePolicy, TimeSpan.Zero);
        IUnchangeableBankAccount account = bank.CreateDepositAccount(accountType, client);

        Assert.Throws<TransactionException>(() => _centralBank.Withdraw(account.Id, new MoneyAmount(1)));
    }

    [Fact]
    public void DepositAccountsWithDifferentBalances_AccountsHaveSuitableInterestOnBalance()
    {
        var suspiciousOperationsLimit = new MoneyAmount(100000);
        INoTransactionalBank bank = GetBank(suspiciousOperationsLimit);
        ICustomer client = GetNotVerifiedCustomer();
        bank.RegisterCustomer(client);
        var depositTerm = TimeSpan.FromDays(90);
        InterestOnBalancePolicy interestOnBalancePolicy = GetInterestOnBalancePolicy();
        IDepositAccountType accountType = bank.AccountTypeManager.CreateDepositAccountType(depositTerm, interestOnBalancePolicy, TimeSpan.Zero);

        Assert.Equal(3, accountType.GetInterestPercent(new MoneyAmount(49_999)));
        Assert.Equal(3.5M, accountType.GetInterestPercent(new MoneyAmount(50_000)));
    }

    [Fact]
    public void AccountWithInterestOnBalance_BalanceIncreasedAfterTimeSkip()
    {
        var suspiciousOperationsLimit = new MoneyAmount(10);
        INoTransactionalBank bank = GetBank(suspiciousOperationsLimit);
        ICustomer client = GetVerifiedCustomer();
        bank.RegisterCustomer(client);
        var interestCalculationPeriod = TimeSpan.FromDays(30);
        const decimal interestOnBalance = 3.65M;
        IDebitAccountType accountType = bank.AccountTypeManager.CreateDebitAccountType(interestOnBalance, interestCalculationPeriod);
        var initialBalance = new MoneyAmount(15);
        IUnchangeableBankAccount account = bank.CreateDebitAccount(accountType, client, initialBalance);

        _clock.SkipDays(30);
        MoneyAmount expected = GetExpectedBalanceAfterInterestCalculation(initialBalance, interestOnBalance);
        Assert.Equal(expected, account.Balance);
    }

    [Fact]
    public void CancelTransaction_AccountBalancesRemainAsBefore()
    {
        var suspiciousOperationsLimit = new MoneyAmount(10);
        INoTransactionalBank bank = GetBank(suspiciousOperationsLimit);
        ICustomer client = GetVerifiedCustomer();
        bank.RegisterCustomer(client);
        IDebitAccountType accountType = bank.AccountTypeManager.CreateDebitAccountType(12, TimeSpan.Zero);
        var initialBalance = new MoneyAmount(15);
        IUnchangeableBankAccount sender = bank.CreateDebitAccount(accountType, client, initialBalance);
        IUnchangeableBankAccount receiver = bank.CreateDebitAccount(accountType, client, initialBalance);

        var transfer = new MoneyAmount(12);
        IOperationInformation operationInformation = _centralBank.Transfer(sender.Id, receiver.Id, transfer);
        Assert.NotEqual(initialBalance, sender.Balance);
        Assert.NotEqual(initialBalance, receiver.Balance);

        _centralBank.CancelTransaction(operationInformation.Id);
        Assert.Equal(initialBalance, sender.Balance);
        Assert.Equal(initialBalance, receiver.Balance);
    }

    [Fact]
    public void CancelCancelledTransaction_ThrowsException()
    {
        var suspiciousOperationsLimit = new MoneyAmount(10);
        INoTransactionalBank bank = GetBank(suspiciousOperationsLimit);
        ICustomer client = GetVerifiedCustomer();
        bank.RegisterCustomer(client);
        IDebitAccountType accountType = bank.AccountTypeManager.CreateDebitAccountType(12, TimeSpan.Zero);
        var initialBalance = new MoneyAmount(15);
        IUnchangeableBankAccount sender = bank.CreateDebitAccount(accountType, client, initialBalance);
        IUnchangeableBankAccount receiver = bank.CreateDebitAccount(accountType, client, initialBalance);

        var transfer = new MoneyAmount(12);
        IOperationInformation operationInformation = _centralBank.Transfer(sender.Id, receiver.Id, transfer);
        Assert.Equal(initialBalance - transfer, sender.Balance);
        Assert.Equal(initialBalance + transfer, receiver.Balance);
        _centralBank.CancelTransaction(operationInformation.Id);

        Assert.Throws<TransactionStateException>(() => _centralBank.CancelTransaction(operationInformation.Id));
    }

    private static ICustomer GetVerifiedCustomer()
    {
        return Customer
            .Builder
            .SetFirstName("Kracker")
            .SetLastName("Slacker")
            .SetAddress(new Address("Vyazemskii per 5/7"))
            .SetPassportData(new PassportData(DateOnly.FromDateTime(DateTime.Now), "58495849"))
            .Build();
    }

    private static ICustomer GetNotVerifiedCustomer()
    {
        return Customer
            .Builder
            .SetFirstName("Kracker")
            .SetLastName("Slacker")
            .Build();
    }

    private static InterestOnBalancePolicy GetInterestOnBalancePolicy()
    {
        return new InterestOnBalancePolicy(new[]
        {
            new InterestOnBalanceLayer(new MoneyAmount(0), 3),
            new InterestOnBalanceLayer(new MoneyAmount(50_000), 3.5M),
            new InterestOnBalanceLayer(new MoneyAmount(100_000), 4),
        });
    }

    private INoTransactionalBank GetBank(MoneyAmount suspiciousOperationsLimit)
    {
        return _centralBank.RegisterBank(new Bank("Krugloff", _accountFactory, suspiciousOperationsLimit, _clock));
    }

    private MoneyAmount GetExpectedBalanceAfterInterestCalculation(MoneyAmount initialBalance, decimal interestOnBalance)
    {
        return initialBalance * (1 + (interestOnBalance / _calendar.GetDaysInYear(_clock.Now.Year)));
    }
}