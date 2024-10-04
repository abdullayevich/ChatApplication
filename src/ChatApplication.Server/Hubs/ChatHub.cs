using ChatApplication.Service.Dtos.Messages;
using ChatApplication.Service.Interfaces;
using Microsoft.AspNetCore.SignalR;


using System.Security.Cryptography.X509Certificates;
using ChatApplication.Service.ViewModels.MessageViewModel;
using ChatApplication.Service.ViewModels.UserViewModels;

namespace ChatApplication.Service.Hubs;

public class ChatHub : Hub
{
	public readonly static List<UserViewModel> _Connections = new List<UserViewModel>();
	private readonly static Dictionary<string, string> _ConnectionsMap = new Dictionary<string, string>();

	private readonly IMessageService _messageService;
	private readonly AppDbContext _dbContext;

	public ChatHub(AppDbContext dbContext, IMessageService messageService)
	{
		this._messageService = messageService;
		this._dbContext = dbContext;
	}
	public async Task SendMessage(string userName, string message)
	{
		if (_ConnectionsMap.TryGetValue(userName, out string userId))
		{
			var sender = _ConnectionsMap.Where(u => userName == IdentityName).First();
			if (!string.IsNullOrEmpty(message.Trim()))
			{
				var messageViewModel = new MessageViewModel()
				{
					MessageContent = message,
					FromUserName = userName,
					Chat = "",
					Timestamp = DateTime.Now
				};

				await Clients.Client(userId).SendAsync("newMessage", messageViewModel);
				await Clients.Caller.SendAsync("newMessage", messageViewModel);
			}
		}

	}
	//=>await Clients.All.SendAsync("ReceiveMessage",userName, Message,Date);
	//public async Task SendToGroup(string groupName, string message)
	//{
	//    var groupChat = await _dbContext.GroupChats.FindAsync(groupName);
	//    if (int.TryParse(Context.UserIdentifier, out int userId))
	//    {
	//        CreateMessageDto messageDto = new CreateMessageDto()
	//        {
	//            SenderId = userId,
	//            MessageContent = message,
	//            GroupId = groupChat.Id

	//        };
	//        await _messageService.SendMessageAsync(messageDto);
	//    }
	//    await Clients.Group(groupName).SendAsync("ReceiveMessage", message);
	//}
	//public async Task SendToUser(int receiverId, string message)
	//{
	//    if (int.TryParse(Context.UserIdentifier, out int userId))
	//    {
	//        CreateMessageDto messageDto = new CreateMessageDto()
	//        {
	//            SenderId = userId,
	//            MessageContent = message,
	//            ReceiverId = receiverId,
	//            GroupId = 0
	//        };
	//        await _messageService.SendMessageAsync(messageDto);
	//    }
	//    await Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", message);
	//}

	public async Task Join(string roomName)
	{
		try
		{
			var user = _Connections.Where(u => u.UserName == IdentityName).FirstOrDefault();
			if (user != null && user.Chat != roomName)
			{
				// Remove user from others list
				if (!string.IsNullOrEmpty(user.Chat))
					await Clients.OthersInGroup(user.Chat).SendAsync("removeUser", user);

				// Join to new chat room
				await Leave(user.Chat);
				await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
				user.Chat = roomName;

				// Tell others to update their list of users
				await Clients.OthersInGroup(roomName).SendAsync("addUser", user);
			}
		}
		catch (Exception ex)
		{
			await Clients.Caller.SendAsync("onError", "You failed to join the chat room!" + ex.Message);
		}
	}

	public async Task Leave(string chatName)
	{
		await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatName);
	}

	public IEnumerable<UserViewModel> GetUsers(string chatName)
	{
		return _Connections.Where(u => u.Chat == chatName).ToList();
	}

	public override Task OnConnectedAsync()
	{
		try
		{
			var user = _dbContext.Users.FindAsync(IdentityName);
			var userViewModel = new UserViewModel()
			{
				Chat = "",
				UserName = user.Result.Username,
				Device = GetDevice()
			};
			if(!_Connections.Any(u => u.UserName == IdentityName))
			{
				_Connections.Add(userViewModel);
				_ConnectionsMap.Add(IdentityName, Context.ConnectionId);
			}
			Clients.Caller.SendAsync("getProfileInfo", userViewModel);
		}
		catch (Exception ex)
		{
			Clients.Caller.SendAsync("onError", "OnConnected:" + ex.Message);
		}
		return base.OnConnectedAsync();
	}

	public override Task OnDisconnectedAsync(Exception? exception)
	{
		try
		{
			var user = _Connections.Where(u => u.UserName == IdentityName).First();
			_Connections.Remove(user);

			Clients.OthersInGroup(user.Chat).SendAsync("removeUser", user);

			_ConnectionsMap.Remove(user.UserName);
		}
		catch (Exception ex)
		{
			Clients.Caller.SendAsync("onError", "OnDisconnected: " + ex.Message);
		}
		return base.OnDisconnectedAsync(exception);
	}
	private string IdentityName
	{
		get { return Context.User.Identity.Name; }
	}
	private string GetDevice()
	{
		var device = Context.GetHttpContext().Request.Headers["Device"].ToString();
		if (!string.IsNullOrEmpty(device) && (device.Equals("Desktop") || device.Equals("Mobile")))
			return device;

		return "Web";
	}
}
