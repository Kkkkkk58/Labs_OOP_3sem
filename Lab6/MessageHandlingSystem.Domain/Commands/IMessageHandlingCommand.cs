using MessageHandlingSystem.Domain.Messages;

namespace MessageHandlingSystem.Domain.Commands;

public interface IMessageHandlingCommand
{
    void Execute(Message message);
}