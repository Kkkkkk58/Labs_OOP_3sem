using MediatR;
using MessageHandlingSystem.Application.Contracts.Reports;
using MessageHandlingSystem.Application.Dto.Reports;
using MessageHandlingSystem.WebAPI.Models.Reports;
using Microsoft.AspNetCore.Mvc;

namespace MessageHandlingSystem.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReportController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost("[action]")]
    public async Task<ActionResult<ReportDto>> CreateAsync([FromBody] MakeReportModel model)
    {
        var command = new MakeReport.Command(model.ManagerId, model.From, model.To);
        MakeReport.Response response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Report);
    }
}