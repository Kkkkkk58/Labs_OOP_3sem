namespace MessageHandlingSystem.WebAPI.Models.Employees;

public record RemoveManagerSubordinateModel(Guid ManagerId, Guid SubordinateId);