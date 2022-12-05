using Banks.AccountTypeManager.Abstractions;
using Banks.AccountTypes.Abstractions;
using Banks.BankAccounts.Abstractions;
using Banks.Entities.Abstractions;
using Banks.EventArgs;
using Banks.Exceptions;
using Banks.Models;
using Banks.Models.Abstractions;
using Banks.Tools.Abstractions;

namespace Banks.Entities;

public class Bank : IBank, IEquatable<Bank>
{
    private readonly IAccountFactory _accountFactory;
    private readonly List<IBankAccount> _accounts;
    private readonly List<ISubscriber<CustomerAccountChangesEventArgs>> _subscribers;
    private readonly List<ICustomer> _customers;
    private readonly IClock _clock;

    public Bank(string name, IAccountFactory accountFactory, MoneyAmount suspiciousOperationsLimit, IClock clock)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(accountFactory);
        ArgumentNullException.ThrowIfNull(clock);

        Id = Guid.NewGuid();
        Name = name;
        _accountFactory = accountFactory;
        _accounts = new List<IBankAccount>();
        _subscribers = new List<ISubscriber<CustomerAccountChangesEventArgs>>();
        _customers = new List<ICustomer>();
        _clock = clock;
        AccountTypeManager = new AccountTypeManager.AccountTypeManager(suspiciousOperationsLimit, NotifySubscribers);
    }

    public Guid Id { get; }
    public string Name { get; }
    public MoneyAmount SuspiciousAccountsOperationsLimit => AccountTypeManager.SuspiciousAccountsOperationsLimit;
    public IAccountTypeManager AccountTypeManager { get; }
    public IReadOnlyCollection<ICustomer> Customers => _customers.AsReadOnly();

    public ICustomer RegisterCustomer(ICustomer customer)
    {
        ArgumentNullException.ThrowIfNull(customer);
        if (_customers.Contains(customer))
            throw BankException.CustomerAlreadyExists(Id, customer.Id);

        _customers.Add(customer);
        Subscribe(customer);
        return customer;
    }

    public void Subscribe(ISubscriber<CustomerAccountChangesEventArgs> subscriber)
    {
        ArgumentNullException.ThrowIfNull(subscriber);
        if (_subscribers.Contains(subscriber))
            throw SubscriptionException.AlreadySubscribed(subscriber.Id);
        if (_customers.All(customer => !customer.Id.Equals(subscriber.Id)))
            throw BankException.CustomerNotFound(subscriber.Id);

        _subscribers.Add(subscriber);
    }

    public void Unsubscribe(ISubscriber<CustomerAccountChangesEventArgs> subscriber)
    {
        ArgumentNullException.ThrowIfNull(subscriber);
        if (!_subscribers.Remove(subscriber))
            throw SubscriptionException.SubscriberNotFound(subscriber.Id);
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
        return FindAccount(accountId) ?? throw BankException.AccountNotFound(accountId);
    }

    public ICommandExecutingBankAccount GetExecutingAccount(Guid id)
    {
        return _accounts.Single(account => account.Id.Equals(id));
    }

    public IUnchangeableBankAccount CreateDebitAccount(IAccountType type, ICustomer customer, MoneyAmount? balance = null)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(customer);

        if (type is not IDebitAccountType debitType)
            throw BankException.InvalidAccountTypeCreation();

        IBankAccount account =
            _accountFactory.CreateDebitAccount(debitType, customer, balance);
        _accounts.Add(account);

        return account;
    }

    public IUnchangeableBankAccount CreateDepositAccount(IAccountType type, ICustomer customer, MoneyAmount? balance = null)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(customer);

        if (type is not IDepositAccountType depositType)
            throw BankException.InvalidAccountTypeCreation();

        IBankAccount account =
            _accountFactory.CreateDepositAccount(depositType, customer, balance);
        _accounts.Add(account);

        return account;
    }

    public IUnchangeableBankAccount CreateCreditAccount(IAccountType type, ICustomer customer, MoneyAmount? balance = null)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(customer);

        if (type is not ICreditAccountType creditType)
            throw BankException.InvalidAccountTypeCreation();
        IBankAccount account =
            _accountFactory.CreateCreditAccount(creditType, customer, balance);
        _accounts.Add(account);

        return account;
    }

    public bool Equals(Bank? other)
    {
        return other is not null && Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Bank);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    private void NotifySubscribers(object? sender, BankTypeChangesEventArgs eventArgs)
    {
        ArgumentNullException.ThrowIfNull(eventArgs);

        Message message = CreateMessage(eventArgs);
        var messageEventArgs = new CustomerAccountChangesEventArgs(message);

        foreach (ISubscriber<CustomerAccountChangesEventArgs> subscriber in _subscribers)
        {
            NotifySubscriberWithSuitableAccount(subscriber, messageEventArgs, eventArgs.AccountType);
        }
    }

    private void NotifySubscriberWithSuitableAccount(
        ISubscriber<CustomerAccountChangesEventArgs> subscriber,
        CustomerAccountChangesEventArgs messageEventArgs,
        IAccountType accountType)
    {
        ICustomer customer = _customers.Single(customer => customer.Id.Equals(subscriber.Id));
        if (GetAccounts(customer.Id).Any(account => account.Type.Equals(accountType)))
        {
            subscriber.Update(this, messageEventArgs);
        }
    }

    private Message CreateMessage(BankTypeChangesEventArgs eventArgs)
    {
        return new Message(Name, $"Changes in your account type {eventArgs.AccountType.Id} details", eventArgs.UpdateInfo, _clock.Now);
    }
}