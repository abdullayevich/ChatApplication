using ChatApplication.Domain.Entities;
using ChatApplication.Service.Dtos.Messages;
using ChatApplication.Service.Interfaces;
using ChatApplication.Service.ViewModels.UserViewModels;
using ChatApplication.Web.Controllers;
using DocumentFormat.OpenXml.InkML;
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
        public async Task SendMessageToUser(string toUser, string message)
        {
            string fromUser = Context.UserIdentifier!;
            var senderId = int.Parse(_accessor.HttpContext!.User.FindFirst("Id")!.Value);
            var user = (_accessor.HttpContext!.User.FindFirst("UserName")!.Value).ToString();
            var timeStamp = DateTime.UtcNow.AddHours(5);
            var newMessage = new CreateMessageDto
            {
                GroupName = "",
                MessageContent = message,
                ReceiverName = toUser,
                SenderId = senderId
            };
            var response = await _httpClient.PostAsJsonAsync<CreateMessageDto>("https://localhost:7096/api/Message/send-message", newMessage);
            await Clients.Users(new[] { toUser, fromUser }).SendAsync("ReceiveMessageToUser", user, message, timeStamp);
        }
        public async Task SendMessage(string groupName, string message)
        {
            var senderId = int.Parse(_accessor.HttpContext!.User.FindFirst("Id")!.Value);
            var user = (_accessor.HttpContext!.User.FindFirst("UserName")!.Value).ToString();
            var fullMessage = $"{user}: {message}";
            var newMessage = new CreateMessageDto
            {
                GroupName = groupName,
                MessageContent = message,
                ReceiverId = 0,
                SenderId = senderId,
                ReceiverName = ""
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

        public override Task OnConnectedAsync()
        {
            string userId = (_accessor.HttpContext!.User.FindFirst("UserName").Value).ToString();
            Groups.AddToGroupAsync(Context.ConnectionId, userId);
            return base.OnConnectedAsync();
        }
    }

}
