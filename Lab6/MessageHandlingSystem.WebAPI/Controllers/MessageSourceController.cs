using MediatR;
using MessageHandlingSystem.Application.Contracts.MessageSources;
using MessageHandlingSystem.Application.Dto.MessageSources;
using MessageHandlingSystem.WebAPI.Models.MessageSources;
using Microsoft.AspNetCore.Mvc;

namespace MessageHandlingSystem.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageSourceController : ControllerBase
{
    private readonly IMediator _mediator;

    public MessageSourceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost("[action]")]
    public async Task<ActionResult<MessageSourceDto>> CreateEmailSourceAsync(
        [FromBody] CreateEmailMessageSourceModel model)
    {
        var command = new CreateEmailMessageSource.Command(model.EmailAddress);
        CreateEmailMessageSource.Response response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Source);
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<MessageSourceDto>> CreateMessengerSourceAsync(
        [FromBody] CreateMessengerMessageSourceModel model)
    {
        var command = new CreateMessengerMessageSource.Command(model.UserName);
        CreateMessengerMessageSource.Response response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Source);
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<MessageSourceDto>> CreatePhoneSourceAsync(
        [FromBody] CreatePhoneMessageSourceModel model)
    {
        var command = new CreatePhoneMessageSource.Command(model.PhoneNumber);
        CreatePhoneMessageSource.Response response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Source);
    }
}