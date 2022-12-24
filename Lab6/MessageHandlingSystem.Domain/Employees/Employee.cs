using MessageHandlingSystem.Domain.Accounts;
using MessageHandlingSystem.Domain.Commands;
using MessageHandlingSystem.Domain.Messages;
using MessageHandlingSystem.Domain.Reports;
using RichEntity.Annotations;

namespace MessageHandlingSystem.Domain.Employees;

public abstract partial class Employee : IEntity<Guid>
{
    private readonly List<Account> _accounts;
    private readonly List<Message> _handledMessages;

    protected Employee(Guid id, string name)
    {
        Id = id;
        Name = name;
        _accounts = new List<Account>();
        _handledMessages = new List<Message>();
    }

    public string Name { get; init; }
    public virtual IReadOnlyCollection<Account> Accounts => _accounts;
    public virtual IReadOnlyCollection<Message> HandledMessages => _handledMessages;

    public virtual void HandleMessage(Guid accountId, Guid messageId, IMessageHandlingCommand command, DateTime time)
    {
        Account account = _accounts.Single(a => a.Id.Equals(accountId));
        Message message = account.LoadedMessages.Single(m => m.Id.Equals(messageId));
        message.Handle(command, time);
        _handledMessages.Add(message);
    }

    public Account AddAccount(Account account)
    {
        if (_accounts.Contains(account))
            throw new NotImplementedException();

        _accounts.Add(account);
        return account;
    }

    public void RemoveAccount(Account account)
    {
        if (!_accounts.Remove(account))
            throw new NotImplementedException();
    }

    public abstract void Accept(IReportingVisitor reportingVisitor);
}
