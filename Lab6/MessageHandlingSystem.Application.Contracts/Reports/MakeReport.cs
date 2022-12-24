using MediatR;
using MessageHandlingSystem.Application.Dto.Reports;

namespace MessageHandlingSystem.Application.Contracts.Reports;

public static class MakeReport
{
    public record struct Command(Guid ManagerId, DateTime From, DateTime To) : IRequest<Response>;
    public record struct Response(ReportDto Report);
}