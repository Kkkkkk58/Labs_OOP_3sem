using System.Runtime.CompilerServices;
using MediatR;
using MessageHandlingSystem.Application.Contracts.Employees;
using MessageHandlingSystem.Application.Dto.Accounts;
using MessageHandlingSystem.Application.Dto.Employees;
using MessageHandlingSystem.Application.Dto.Messages;
using MessageHandlingSystem.WebAPI.Models.Employees;
using Microsoft.AspNetCore.Mvc;

namespace MessageHandlingSystem.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost("create/manager/{name}")]
    public async Task<ActionResult<ManagerDto>> CreateManagerAsync([FromBody] CreateManagerModel model)
    {
        var command = new CreateManager.Command(model.Name);
        CreateManager.Response response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Manager);
    }

    [HttpPost("create/subordinate/{name}")]
    public async Task<ActionResult<SubordinateDto>> CreateSubordinateAsync([FromBody] CreateSubordinateModel model)
    {
        var command = new CreateSubordinate.Command(model.Name);
        CreateSubordinate.Response response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Subordinate);
    }

    [HttpPatch("{id:guid}/account/{account_id:guid}/add")]
    public async Task<ActionResult> AddAccountAsync([FromBody] AddEmployeeAccountModel model)
    {
        var command = new AddEmployeeAccount.Command(model.EmployeeId, model.AccountId);
        await _mediator.Send(command, CancellationToken);

        return Ok();
    }

    [HttpPatch("{id:guid}/account/{account_id:guid}/remove")]
    public async Task<ActionResult> RemoveAccountAsync([FromBody] RemoveEmployeeAccountModel model)
    {
        var command = new RemoveEmployeeAccount.Command(model.EmployeeId, model.AccountId);
        await _mediator.Send(command, CancellationToken);

        return Ok();
    }

    [HttpPatch("{manager_id:guid}/subordinate/add/{subordinate_id:guid}")]
    public async Task<ActionResult> AddSubordinateAsync([FromBody] AddManagerSubordinateModel model)
    {
        var command = new AddManagerSubordinate.Command(model.ManagerId, model.SubordinateId);
        await _mediator.Send(command, CancellationToken);

        return Ok();
    }

    [HttpPatch("{manager_id:guid}/subordinate/remove/{subordinate_id:guid}")]
    public async Task<ActionResult> RemoveSubordinateAsync([FromBody] RemoveManagerSubordinateModel model)
    {
        var command = new RemoveManagerSubordinate.Command(model.ManagerId, model.SubordinateId);
        await _mediator.Send(command, CancellationToken);

        return Ok();
    }

    [HttpGet("{id:guid}/accounts")]
    public async Task<ActionResult<IEnumerable<AccountDto>>> GetAccountsAsync([FromBody] GetEmployeeAccountsModel model)
    {
        var query = new GetEmployeeAccounts.Query(model.EmployeeId);
        GetEmployeeAccounts.Response response = await _mediator.Send(query, CancellationToken);

        return Ok(response.Accounts);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EmployeeDto>> GetInfoAsync([FromBody] GetEmployeeInformationModel model)
    {
        var query = new GetEmployeeInformation.Query(model.EmployeeId);
        GetEmployeeInformation.Response response = await _mediator.Send(query, CancellationToken);

        return Ok(response.Employee);
    }

    [HttpGet("{manager_id:guid}/subordinates")]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetSubordinatesAsync(
        [FromBody] GetManagerSubordinatesModel model)
    {
        var query = new GetManagerSubordinates.Query(model.ManagerId);
        GetManagerSubordinates.Response response = await _mediator.Send(query, CancellationToken);

        return Ok(response.Subordinates);
    }

    [HttpPatch("{id:guid}/{account_id:guid}/handle/{message_id:guid}")]
    public async Task<ActionResult<MessageDto>> HandleMessageAsync([FromBody] HandleMessageModel model)
    {
        // TODO make timer
        var command = new HandleMessage.Command(model.EmployeeId, model.AccountId, model.MessageId, DateTime.Now);
        HandleMessage.Response response = await _mediator.Send(command, CancellationToken);

        return Ok(response.HandledMessage);
    }
}