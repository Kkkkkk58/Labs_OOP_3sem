using MediatR;
using MessageHandlingSystem.Application.Abstractions.DataAccess;
using MessageHandlingSystem.Application.Extensions;
using MessageHandlingSystem.Application.Mapping.Reports;
using MessageHandlingSystem.Domain.Employees;
using MessageHandlingSystem.Domain.Reports;
using static MessageHandlingSystem.Application.Contracts.Reports.MakeReport;

namespace MessageHandlingSystem.Application.Reports;

public class MakeReportHandler : IRequestHandler<Command, Response>
{
    private readonly IDataBaseContext _dbContext;

    public MakeReportHandler(IDataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        Manager manager = await _dbContext.Managers.GetEntityAsync(request.ManagerId, cancellationToken);
        Report report = manager.MakeReport(Guid.NewGuid(), request.From, request.To);

        await _dbContext.Reports.AddAsync(report, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new Response(report.AsDto());
    }
}