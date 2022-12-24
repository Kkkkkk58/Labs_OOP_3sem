namespace MessageHandlingSystem.WebAPI.Models.Messages;

public record CreatePhoneMessageModel(DateTime SendingTime, string SenderPhoneNumber, string Content);