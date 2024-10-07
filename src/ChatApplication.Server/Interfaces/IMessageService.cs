using ChatApplication.Domain.Entities;
using ChatApplication.Service.Dtos.Messages;
namespace ChatApplication.Service.Interfaces;

public interface IMessageService
{
    public Task<Message> SendMessageAsync(CreateMessageDto messageDto);
    public Task<IEnumerable<Message>> GetMessagesByUserIdAsync(int userId);
    public Task<IEnumerable<Message>> GetGroupMessagesAsync(string groupName);
}
