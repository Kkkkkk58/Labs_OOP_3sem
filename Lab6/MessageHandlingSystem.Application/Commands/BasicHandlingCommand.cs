using MessageHandlingSystem.Domain.Commands;
using MessageHandlingSystem.Domain.Messages;

namespace MessageHandlingSystem.Application.Commands;

public class BasicHandlingCommand : IMessageHandlingCommand
{
    public void Execute(Message message)
    {
    }
}