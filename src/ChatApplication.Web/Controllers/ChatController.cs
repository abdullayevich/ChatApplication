using ChatApplication.Domain.Entities;
using ChatApplication.Service.ViewModels.MessageViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ChatApplication.Web.Controllers;
[Authorize]
public class ChatController : Controller
{
	private readonly IHttpContextAccessor _contextAccessor;
	private readonly HttpClient _httpClient;

    public ChatController(HttpClient httpClient, IHttpContextAccessor accessor)
    {
		_contextAccessor = accessor;
		_httpClient = httpClient;
    }
    public async Task<ViewResult> Index()
    {
		ViewBag.UserName = _contextAccessor.HttpContext?.User.FindFirst("UserName")?.Value;
		var groupChats = await _httpClient.GetFromJsonAsync<IList<GroupChat>>("https://localhost:7096/api/GroupChat/get-all-groupChat");
        return View("Index", groupChats);
    }
    
    [HttpGet]
    public async Task<IActionResult> ChatRoom()
    {
		ViewBag.UserName = _contextAccessor.HttpContext?.User.FindFirst("UserName")?.Value;
		return View("GroupChat");
    }
}
