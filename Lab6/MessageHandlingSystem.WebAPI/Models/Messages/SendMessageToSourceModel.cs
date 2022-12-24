namespace MessageHandlingSystem.WebAPI.Models.Messages;

public record SendMessageToSourceModel(Guid MessageId, Guid MessageSourceId);