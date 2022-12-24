namespace MessageHandlingSystem.WebAPI.Models.Reports;

public record MakeReportModel(Guid ManagerId, DateTime From, DateTime To);