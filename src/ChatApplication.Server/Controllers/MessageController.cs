using ChatApplication.Service.Dtos.Messages;
using ChatApplication.Service.Interfaces;
using ChatApplication.Service.ViewModels.MessageViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Service.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly IMessageService _messageService;

    public MessageController(IMessageService messageService)
    {
        this._messageService = messageService;
    }

    [HttpGet("get-all/{groupName}")]
    public async Task<IActionResult> GetGroupMessagesAsync(string groupName)
    {
        var groupMessages = await _messageService.GetGroupMessagesAsync(groupName);
        var result = groupMessages.Select(x => new MessageViewModel
        {
            Id = x.Id,
            FromUserName = x.Sender.Username,
            MessageContent = x.MessageContent,
            Timestamp = x.SentAt
        });
        return Ok(result);
    }

    [HttpGet("user-all-message/{id}")]
    public async Task<IActionResult> GetMessagesByUserIdAsync(int id)
    {
        var result = await _messageService.GetMessagesByUserIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    [Route("send-message")]
    public async Task<IActionResult> SendMessageAsync(CreateMessageDto messageDto)
    {
        var result = await _messageService.SendMessageAsync(messageDto);
        return Ok(result);
    }
}
