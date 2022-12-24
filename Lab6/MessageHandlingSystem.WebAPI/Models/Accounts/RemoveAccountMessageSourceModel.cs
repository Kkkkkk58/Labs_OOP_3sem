namespace MessageHandlingSystem.WebAPI.Models.Accounts;

public record RemoveAccountMessageSourceModel(Guid EmployeeId, Guid AccountId, Guid MessageSourceId);