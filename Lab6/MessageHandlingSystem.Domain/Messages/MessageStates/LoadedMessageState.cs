using MessageHandlingSystem.Domain.Commands;
using MessageHandlingSystem.Domain.Common.Exceptions;

namespace MessageHandlingSystem.Domain.Messages.MessageStates;

public class LoadedMessageState : IMessageState
{
    public MessageStateKind Kind => MessageStateKind.Loaded;

    public void Load(Message message)
    {
        throw MessageStateException.InvalidMessageState();
    }

    public void Handle(Message message, IMessageHandlingCommand command)
    {
        message.SetState(new HandledMessageState());
    }
}