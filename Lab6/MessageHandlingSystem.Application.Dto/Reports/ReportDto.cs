namespace MessageHandlingSystem.Application.Dto.Reports;

public record ReportDto(Guid Id, Guid AuthorId, DateTime From, DateTime To, IReadOnlyDictionary<Guid, int> MessagesHandledByEmployee, IReadOnlyDictionary<Guid, int> MessagesReceivedBySource, int TotalMessages);