using MessageHandlingSystem.Domain.Commands;

namespace MessageHandlingSystem.Domain.Messages.MessageStates;

public class NewMessageState : IMessageState
{
    public MessageStateKind Kind => MessageStateKind.New;

    public void Load(Message message)
    {
        message.SetState(new LoadedMessageState());
    }

    public void Handle(Message message, IMessageHandlingCommand command)
    {
        throw new NotImplementedException();
    }
}