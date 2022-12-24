namespace MessageHandlingSystem.WebAPI.Models.Accounts;

public record AddAccountMessageSourceModel(Guid EmployeeId, Guid AccountId, Guid MessageSourceId);