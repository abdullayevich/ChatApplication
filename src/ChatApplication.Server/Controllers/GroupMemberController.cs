using ChatApplication.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Service.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupMemberController : ControllerBase
{
    private readonly IGroupMemberService _memberService;

    public GroupMemberController(IGroupMemberService memberService)
    {
        this._memberService = memberService;
    }
    // Bitta userning hamma groupchatlari chiqadi
    [HttpGet("get-all-groupChat/{userId}")]
    public async Task<IActionResult> GetAllGroupByUserId(int userId)
    {
        var result = await _memberService.GetAllGroupByUserId(userId);
        return Ok(result);
    }
    // Guruh bo'yicha hamma memberlarni olib beradi
    [HttpGet("get-all-members/{groupId}")]
    public async Task<IActionResult> GetGroupMembersAsync(int groupId)
    {
        var result = await _memberService.GetGroupMembersAsync(groupId);
        return Ok(result);
    }
    [HttpGet]
    [Route("is-member")]
    public async Task<IActionResult> IsUserInGroupAsync(int groupId, int userId)
    {
        var result = await _memberService.IsUserInGroupAsync(groupId, userId);
        return Ok(result);
    }
    [HttpGet]
    [Route("join-user")]
    public async Task<IActionResult> AddUserToGroupAsync(int groupId, int userId)
    {
        var result = await _memberService.AddUserToGroupAsync(groupId,userId);
        return Ok(result);
    }
}
