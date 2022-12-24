namespace MessageHandlingSystem.WebAPI.Models.Employees;

public record HandleMessageModel(Guid EmployeeId, Guid AccountId, Guid MessageId, DateTime Time);