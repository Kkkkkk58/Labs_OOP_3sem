using MediatR;
using MessageHandlingSystem.Application.Contracts.Accounts;
using MessageHandlingSystem.Application.Dto.Accounts;
using MessageHandlingSystem.Application.Dto.Messages;
using MessageHandlingSystem.Application.Dto.MessageSources;
using MessageHandlingSystem.WebAPI.Models.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace MessageHandlingSystem.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost("create")]
    public async Task<ActionResult<AccountDto>> CreateAsync()
    {
        var command = default(CreateAccount.Command);
        CreateAccount.Response response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Account);
    }

    [HttpPatch("source/add/{employee_id:guid},{account_id:guid},{message_source_id:guid}")]
    public async Task<ActionResult> AddMessageSourceAsync([FromBody] AddAccountMessageSourceModel model)
    {
        var command = new AddAccountMessageSource.Command(model.EmployeeId, model.AccountId, model.MessageSourceId);
        await _mediator.Send(command, CancellationToken);

        return Ok();
    }

    [HttpPatch("source/remove/{employee_id:guid},{account_id:guid},{message_source_id:guid}")]
    public async Task<ActionResult> RemoveMessageSourceAsync([FromBody] RemoveAccountMessageSourceModel model)
    {
        var command = new RemoveAccountMessageSource.Command(model.EmployeeId, model.AccountId, model.MessageSourceId);
        await _mediator.Send(command, CancellationToken);

        return Ok();
    }

    [HttpGet("loaded")]
    public async Task<ActionResult<IEnumerable<MessageDto>>> GetLoadedMessagesAsync(
        [FromBody] GetAccountLoadedMessagesModel model)
    {
        var command = new GetAccountLoadedMessages.Query(model.EmployeeId, model.AccountId);
        GetAccountLoadedMessages.Response response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Messages);
    }

    [HttpGet("source/get")]
    public async Task<ActionResult<IEnumerable<MessageSourceDto>>> GetMessageSourcesAsync(
        [FromBody] GetAccountMessageSourcesModel model)
    {
        var command = new GetAccountMessageSources.Query(model.EmployeeId, model.AccountId);
        GetAccountMessageSources.Response response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Accounts);
    }

    [HttpPatch("load_msg")]
    public async Task<ActionResult> LoadMessagesAsync([FromBody] LoadAccountMessagesModel model)
    {
        var command = new LoadAccountMessages.Command(model.EmployeeId, model.AccountId, model.MessageSourceId);
        await _mediator.Send(command, CancellationToken);

        return Ok();
    }
}