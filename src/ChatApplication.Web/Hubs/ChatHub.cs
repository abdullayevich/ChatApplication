using ChatApplication.Domain.Entities;
using ChatApplication.Service.Dtos.Messages;
using ChatApplication.Service.Interfaces;
using ChatApplication.Service.ViewModels.UserViewModels;
using ChatApplication.Web.Controllers;
using Microsoft.AspNetCore.SignalR;
using System.Net.Http.Json;

namespace ChatApplication.Web.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly HttpClient _httpClient;
        public ChatHub(HttpClient httpClient, IHttpContextAccessor accessor)
        {
            _accessor = accessor;
            _httpClient = httpClient;
        }
        public async Task SendMessage(string groupName, string message)
        {
            var senderId = int.Parse(_accessor.HttpContext!.User.FindFirst("Id").Value);
            var user = (_accessor.HttpContext!.User.FindFirst("UserName").Value).ToString();
            var fullMessage = $"{user}: {message}";
            var newMessage = new CreateMessageDto
            {
                GroupName = groupName,
                MessageContent = message,
                ReceiverId = 0,
                SenderId = senderId,
            };
            var timestamp = DateTime.UtcNow.AddHours(5);
            var response = await _httpClient.PostAsJsonAsync<CreateMessageDto>("https://localhost:7096/api/Message/send-message", newMessage);

            await Clients.Group(groupName).SendAsync("ReceiveMessage", user, message,timestamp);
        }

        public Task JoinGroup(string groupName)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public Task LeaveGroup(string groupName)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }

}
