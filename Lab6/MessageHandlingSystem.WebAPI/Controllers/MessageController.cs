using System.Reflection.Metadata.Ecma335;
using MediatR;
using MessageHandlingSystem.Application.Contracts.Messages;
using MessageHandlingSystem.Application.Dto.Messages;
using MessageHandlingSystem.WebAPI.Models.Messages;
using Microsoft.AspNetCore.Mvc;

namespace MessageHandlingSystem.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly IMediator _mediator;

    public MessageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost("create/email/{sender_address},{topic},{content}")]
    public async Task<ActionResult<EmailMessageDto>> CreateEmailMessageAsync([FromBody] CreateEmailMessageModel model)
    {
        var command = new CreateEmailMessage.Command(DateTime.Now, model.SenderAddress, model.Topic, model.Content);
        CreateEmailMessage.Response response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Message);
    }

    [HttpPost("create/phone/{sender_phone},{content}")]
    public async Task<ActionResult<PhoneMessageDto>> CreatePhoneMessageAsync([FromBody] CreatePhoneMessageModel model)
    {
        var command = new CreatePhoneMessage.Command(DateTime.Now, model.SenderPhoneNumber, model.Content);
        CreatePhoneMessage.Response response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Message);
    }

    [HttpPost("create/messenger/{sender_username},{content}")]
    public async Task<ActionResult<MessengerMessageDto>> CreateMessengerMessageAsync([FromBody] CreateMessengerMessageModel model)
    {
        var command = new CreateMessengerMessage.Command(DateTime.Now, model.SenderUserName, model.Content);
        CreateMessengerMessage.Response response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Message);
    }

    [HttpPatch("{id:guid}/send/{source_id:guid}")]
    public async Task<ActionResult<MessageDto>> SendMessageToSource([FromBody] SendMessageToSourceModel model)
    {
        var command = new SendMessageToSource.Command(model.MessageId, model.MessageSourceId);
        await _mediator.Send(command, CancellationToken);

        return Ok();
    }
}