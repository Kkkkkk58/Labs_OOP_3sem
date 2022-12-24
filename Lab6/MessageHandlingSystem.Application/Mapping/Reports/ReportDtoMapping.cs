using MessageHandlingSystem.Application.Dto.Reports;
using MessageHandlingSystem.Domain.Reports;

namespace MessageHandlingSystem.Application.Mapping.Reports;

public static class ReportDtoMapping
{
    public static ReportDto AsDto(this Report report)
    {
        return new ReportDto(report.Id, report.AuthorId, report.From, report.To, report.MessagesHandledByEmployee, report.MessagesReceivedBySource, report.TotalMessages);
    }
}