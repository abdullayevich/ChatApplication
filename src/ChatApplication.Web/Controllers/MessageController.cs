using ChatApplication.Domain.Entities;
using ChatApplication.Service.Dtos.Messages;
using ChatApplication.Service.Dtos.Users;
using ChatApplication.Service.ViewModels.MessageViewModel;
using ChatApplication.Web.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Net.Http;
using System.Security.Claims;

namespace ChatApplication.Web.Controllers;
[Authorize]
public class MessageController : Controller
{
    private readonly IHttpContextAccessor _accessor;
    private readonly HttpClient _httpClient;

    public MessageController(IHttpContextAccessor accessor, HttpClient httpClient)
    {
        this._accessor = accessor;
        this._httpClient = httpClient;
    }

    public IActionResult Index()
    {
		ViewBag.UserName = _accessor.HttpContext?.User.FindFirst("UserName")?.Value;

		return View();
    }

    [HttpGet("GetMessages/{groupName}")]
    public async Task<IActionResult> GetMessages(string groupName)
    {
		ViewBag.UserName = _accessor.HttpContext?.User.FindFirst("UserName")?.Value;
		var groupMessages = await _httpClient.GetFromJsonAsync<IList<MessageViewModel>>($"https://localhost:7096/api/Message/get-all/{groupName}");


        return Ok(groupMessages);
    }
    [HttpGet("GetUserMessages/{userId}")]
    public async Task<IActionResult> GetUserMessages(int userId)
    {
		ViewBag.UserName = _accessor.HttpContext?.User.FindFirst("UserName")?.Value;

		var senderId = int.Parse(_accessor.HttpContext!.User.FindFirst("Id").Value);
        var userName = (_accessor.HttpContext!.User.FindFirst("UserName").Value).ToString();
        var userMessages = await _httpClient.GetFromJsonAsync<IList<Message>>($"https://localhost:7096/api/Message/user-all-message/{userId}");
        var result = userMessages.Where(x => x.ReceiverId == userId && x.SenderId == senderId || x.SenderId == userId && x.ReceiverId == senderId)
                                    .Select(x => new MessageViewModel
                                    {

                                        FromUserName = x.SenderId == userId ? userName : "",
                                        Timestamp = x.SentAt,
                                        Id = x.Id,
                                        MessageContent = x.MessageContent
                                    });
        return Ok(result);
    }
}
