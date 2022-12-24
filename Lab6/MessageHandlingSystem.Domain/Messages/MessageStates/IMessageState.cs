using MessageHandlingSystem.Domain.Commands;

namespace MessageHandlingSystem.Domain.Messages.MessageStates;

public interface IMessageState
{
    MessageStateKind Kind { get; }
    void Load(Message message);
    void Handle(Message message, IMessageHandlingCommand command);
}