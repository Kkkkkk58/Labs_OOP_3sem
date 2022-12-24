namespace MessageHandlingSystem.WebAPI.Models.Messages;

public record CreateEmailMessageModel(DateTime SendingTime, string SenderAddress, string Topic, string Content);