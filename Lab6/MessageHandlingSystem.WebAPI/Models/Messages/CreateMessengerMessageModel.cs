namespace MessageHandlingSystem.WebAPI.Models.Messages;

public record CreateMessengerMessageModel(DateTime SendingTime, string SenderUserName, string Content);