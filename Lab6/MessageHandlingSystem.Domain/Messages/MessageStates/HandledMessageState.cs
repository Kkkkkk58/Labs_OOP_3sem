using MessageHandlingSystem.Domain.Commands;

namespace MessageHandlingSystem.Domain.Messages.MessageStates;

public class HandledMessageState : IMessageState
{
    public MessageStateKind Kind => MessageStateKind.Handled;

    public void Load(Message message)
    {
        throw new NotImplementedException();
    }

    public void Handle(Message message, IMessageHandlingCommand command)
    {
        throw new NotImplementedException();
    }
}