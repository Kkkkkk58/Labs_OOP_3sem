using Banks.BankAccounts.Abstractions;
using Banks.Entities.Abstractions;
using Banks.Models;
using Banks.Models.Abstractions;
using Banks.Models.AccountTypes.Abstractions;
using Banks.Tools.Abstractions;

namespace Banks.Entities;

public class Bank : IBank
{
    private readonly IAccountFactory _accountFactory;
    private readonly List<IBankAccount> _accounts;
    private readonly List<ISubscriber<CustomerAccountChangesEventArgs>> _subscribers;
    private readonly List<ICustomer> _customers;
    private readonly IClock _clock;

    public Bank(string name, IAccountFactory accountFactory, MoneyAmount suspiciousOperationsLimit, IClock clock)
    {
        Id = Guid.NewGuid();
        Name = name;
        _accountFactory = accountFactory;
        _accounts = new List<IBankAccount>();
        _subscribers = new List<ISubscriber<CustomerAccountChangesEventArgs>>();
        _customers = new List<ICustomer>();
        _clock = clock;
        AccountTypeManager = new AccountTypeManager(suspiciousOperationsLimit, NotifySubscribers);
    }

    public Guid Id { get; }
    public string Name { get; }
    public MoneyAmount SuspiciousAccountsOperationsLimit => AccountTypeManager.SuspiciousAccountsOperationsLimit;
    public IAccountTypeManager AccountTypeManager { get; }
    public IReadOnlyCollection<ICustomer> Customers => _customers.AsReadOnly();

    public ICustomer RegisterCustomer(ICustomer customer)
    {
        if (_customers.Contains(customer))
            throw new NotImplementedException();
        _customers.Add(customer);
        Subscribe(customer);
        return customer;
    }

    public void Subscribe(ISubscriber<CustomerAccountChangesEventArgs> subscriber)
    {
        if (_subscribers.Contains(subscriber))
            throw new NotImplementedException();
        if (_customers.All(customer => !customer.Id.Equals(subscriber.Id)))
            throw new NotImplementedException();

        _subscribers.Add(subscriber);
    }

    public void Unsubscribe(ISubscriber<CustomerAccountChangesEventArgs> subscriber)
    {
        if (!_subscribers.Remove(subscriber))
            throw new NotImplementedException();
    }

    public void SetSuspiciousAccountsOperationsLimit(MoneyAmount limit)
    {
        AccountTypeManager.SetSuspiciousOperationsLimit(limit);
    }

    public IReadOnlyCollection<IUnchangeableBankAccount> GetAccounts(Guid accountHolderId)
    {
        return _accounts
            .Where(account => account.Holder.Id.Equals(accountHolderId))
            .ToList()
            .AsReadOnly();
    }

    public IUnchangeableBankAccount? FindAccount(Guid accountId)
    {
        return _accounts.SingleOrDefault(account => account.Id.Equals(accountId));
    }

    public IUnchangeableBankAccount GetAccount(Guid accountId)
    {
        IUnchangeableBankAccount? account = FindAccount(accountId);
        return account ?? throw new NotImplementedException();
    }

    public ICommandExecutingBankAccount GetExecutingAccount(Guid id)
    {
        return _accounts.Single(account => account.Id.Equals(id));
    }

    public IUnchangeableBankAccount CreateDebitAccount(IAccountType type, ICustomer customer, MoneyAmount? balance = null)
    {
        if (type is not IDebitAccountType debitType)
            throw new NotImplementedException();

        IBankAccount account =
            _accountFactory.CreateDebitAccount(debitType, customer, balance);
        _accounts.Add(account);

        return account;
    }

    public IUnchangeableBankAccount CreateDepositAccount(IAccountType type, ICustomer customer, MoneyAmount? balance = null)
    {
        if (type is not IDepositAccountType depositType)
            throw new NotImplementedException();

        IBankAccount account =
            _accountFactory.CreateDepositAccount(depositType, customer, balance);
        _accounts.Add(account);

        return account;
    }

    public IUnchangeableBankAccount CreateCreditAccount(IAccountType type, ICustomer customer, MoneyAmount? balance = null)
    {
        if (type is not ICreditAccountType creditType)
            throw new NotImplementedException();
        IBankAccount account =
            _accountFactory.CreateCreditAccount(creditType, customer, balance);
        _accounts.Add(account);

        return account;
    }

    private void NotifySubscribers(object? sender, BankTypeChangesEventArgs eventArgs)
    {
        Message message = CreateMessage(eventArgs);
        var messageEventArgs = new CustomerAccountChangesEventArgs(message);

        foreach (ISubscriber<CustomerAccountChangesEventArgs> subscriber in _subscribers)
        {
            ICustomer customer = _customers.Single(customer => customer.Id.Equals(subscriber.Id));
            if (GetAccounts(customer.Id).Any(account => account.Type.Equals(eventArgs.AccountType)))
            {
                subscriber.Update(this, messageEventArgs);
            }
        }
    }

    private Message CreateMessage(BankTypeChangesEventArgs eventArgs)
    {
        return new Message(Name, "Changes in your account details", eventArgs.UpdateInfo, _clock.Now);
    }
}