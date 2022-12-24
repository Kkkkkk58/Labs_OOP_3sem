using MessageHandlingSystem.Domain.Common.Exceptions;
using MessageHandlingSystem.Domain.Messages;
using MessageHandlingSystem.Domain.Messages.MessageStates;
using MessageHandlingSystem.Domain.MessageSources;
using RichEntity.Annotations;

namespace MessageHandlingSystem.Domain.Accounts;

public partial class Account : IEntity<Guid>
{
    private readonly List<MessageSource> _messageSources;
    private readonly List<Message> _loadedMessages;

    public Account(Guid id)
    {
        Id = id;
        _messageSources = new List<MessageSource>();
        _loadedMessages = new List<Message>();
    }

    public virtual IReadOnlyCollection<MessageSource> MessageSources => _messageSources;
    public virtual IReadOnlyCollection<Message> LoadedMessages => _loadedMessages;

    public void LoadMessages(Guid messageSourceId)
    {
        MessageSource? source = _messageSources.Find(src => src.Id.Equals(messageSourceId));
        if (source is null)
            throw AccountException.MessageSourceNotFound(messageSourceId, Id);

        // TODO Set<Message>
        foreach (Message message in source.ReceivedMessages)
        {
            if (_loadedMessages.Contains(message))
                continue;

            _loadedMessages.Add(message);
            if (message.State.Kind == MessageStateKind.New)
            {
                message.Load();
            }
        }
    }

    public void AddMessageSource(MessageSource messageSource)
    {
        if (_messageSources.Contains(messageSource))
            throw AccountException.MessageSourceAlreadyExists(messageSource.Id, Id);
        _messageSources.Add(messageSource);
    }

    public void RemoveMessageSource(MessageSource messageSource)
    {
        if (!_messageSources.Remove(messageSource))
            throw AccountException.MessageSourceNotFound(messageSource.Id, Id);
    }
}