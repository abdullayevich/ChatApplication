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
        
        [HttpGet("Chat/{groupId}")]
        public async Task<IActionResult> GetMessages(int groupId)
        {
            var groupMessages = await _httpClient.GetFromJsonAsync<IList<MessageViewModel>>($"https://localhost:7096/api/Message/get-all/{groupId}");
            

            //var groupMessages = await _httpClient.GetAsync($"https://localhost:7096/api/Message/get-all/{groupId}");


            return Ok(groupMessages);
        }
    }
}
