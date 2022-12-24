namespace MessageHandlingSystem.WebAPI.Models.Accounts;

public record LoadAccountMessagesModel(Guid EmployeeId, Guid AccountId, Guid MessageSourceId);