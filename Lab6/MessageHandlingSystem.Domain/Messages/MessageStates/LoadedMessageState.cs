using MessageHandlingSystem.Domain.Commands;

namespace MessageHandlingSystem.Domain.Messages.MessageStates;

public class LoadedMessageState : IMessageState
{
    public MessageStateKind Kind => MessageStateKind.Loaded;

    public void Load(Message message)
    {
        throw new NotImplementedException();
    }

    public void Handle(Message message, IMessageHandlingCommand command)
    {
        message.SetState(new HandledMessageState());
    }
}