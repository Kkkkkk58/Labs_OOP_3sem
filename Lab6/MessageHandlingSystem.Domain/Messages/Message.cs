using MessageHandlingSystem.Domain.Commands;
using MessageHandlingSystem.Domain.Messages.MessageStates;
using RichEntity.Annotations;

namespace MessageHandlingSystem.Domain.Messages;

public abstract partial class Message : IEntity<Guid>
{
    protected Message(Guid id, DateTime sendingTime, string content)
    {
        Id = id;
        SendingTime = sendingTime;
        Content = content;
        State = new NewMessageState();
    }

    public DateTime SendingTime { get; init; }
    public DateTime? HandlingTime { get; private set; }
    public string Content { get; init; }
    public IMessageState State { get; private set; }

    public void SetState(IMessageState state)
    {
        State = state;
    }

    public virtual void Load()
    {
        State.Load(this);
    }

    public virtual void Handle(IMessageHandlingCommand command, DateTime time)
    {
        State.Handle(this, command);
        HandlingTime = time;
    }
}