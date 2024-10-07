using ChatApplication.Domain.Entities;
using ChatApplication.Service.ViewModels.MessageViewModel;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ChatApplication.Web.Controllers
{
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
        [Route("ChatRoom")]
        public async Task<IActionResult> ChatRoom()
        {
            return View("GroupChat");
        }
    }
}
