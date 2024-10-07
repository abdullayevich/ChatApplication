using ChatApplication.Domain.Entities;
using ChatApplication.Service.Dtos.Messages;
using ChatApplication.Service.Dtos.Users;
using ChatApplication.Service.ViewModels.MessageViewModel;
using ChatApplication.Web.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Net.Http;
using System.Security.Claims;

namespace ChatApplication.Web.Controllers;

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
        return View();
    }

    //[HttpPost]
    //[Route("SendMessageToGroup")]
    //public async Task<IActionResult> SendMessageToGroup(CreateMessageDto message)
    //{
    //    var res = int.Parse(_accessor.HttpContext!.User.FindFirst("Id").Value);
    //    var senderName = User.FindFirstValue(ClaimTypes.NameIdentifier);
    //    var newMessage = new CreateMessageDto
    //    {
    //        GroupId = message.GroupId,
    //        MessageContent = message.MessageContent,
    //        ReceiverId = 0,
    //        SenderId = res
    //    };
    //    var response = await _httpClient.PostAsJsonAsync<CreateMessageDto>("https://localhost:7096/api/Message/send-message", message);
    //    if (response.IsSuccessStatusCode)
    //    {
    //        Console.WriteLine("Message added");
    //    }

    //    await _hubContext.Clients.Group("New").SendAsync("ReceiveMessage", senderName, newMessage.MessageContent);
    //    return RedirectToAction("Index", new { id = message.ReceiverId });
    //}

    [HttpGet("GetMessages/{groupName}")]
    public async Task<IActionResult> GetMessages(string groupName)
    {
        var groupMessages = await _httpClient.GetFromJsonAsync<IList<MessageViewModel>>($"https://localhost:7096/api/Message/get-all/{groupName}");


        return Ok(groupMessages);
    }
}
