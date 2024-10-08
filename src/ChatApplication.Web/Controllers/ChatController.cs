using ChatApplication.Domain.Entities;
using ChatApplication.Service.ViewModels.MessageViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ChatApplication.Web.Controllers;
[Authorize]
public class ChatController : Controller
{
    private readonly HttpClient _httpClient;

    public ChatController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<ViewResult> Index()
    {
        var groupChats = await _httpClient.GetFromJsonAsync<IList<GroupChat>>("https://localhost:7096/api/GroupChat/get-all-groupChat");
        return View("Index", groupChats);
    }
    
    [HttpGet]
    public async Task<IActionResult> ChatRoom()
    {
        return View("GroupChat");
    }
}
