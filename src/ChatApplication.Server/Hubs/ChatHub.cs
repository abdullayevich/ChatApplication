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

    public async Task SendMessage(string groupId, string message, string fromUserName)
    {
        await Clients.Group(groupId).SendAsync("ReceiveMessage", fromUserName, message);
    }

    public async Task JoinGroup(string groupId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
    }

    public async Task LeaveGroup(string groupId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId);
    }
}
