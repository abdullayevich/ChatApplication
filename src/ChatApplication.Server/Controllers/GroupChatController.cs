using ChatApplication.Service.Dtos.Groups;
using ChatApplication.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Service.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupChatController : ControllerBase
{
    private readonly IGroupChatService _chatService;

    public GroupChatController(IGroupChatService chatService)
    {
        this._chatService = chatService;
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateGroupChatAsync(CreateGroupDto groupDto)
    {
        var result = await _chatService.CreateGroupChatAsync(groupDto);
        return Ok(result);
    }

    // Bitta guruh va uning barcha messagelari
    [HttpGet("{groupId}")]
    public async Task<IActionResult> GetGroupChatByIdAsync(int groupId)
    {
        var result = await _chatService.GetGroupChatByIdAsync(groupId);
        return Ok(result);
    }

    [HttpGet]
    [Route("get-all-groupChat")]
    public async Task<IActionResult> GetAllGroupChatsAsync()
    {
        var result = await _chatService.GetAllGroupChatsAsync();
        return Ok(result);
    }

}
