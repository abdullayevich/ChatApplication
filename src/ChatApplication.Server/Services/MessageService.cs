using ChatApplication.Domain.Entities;
using ChatApplication.Service.Dtos.Messages;
using ChatApplication.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace ChatApplication.Service.Services;

public class MessageService : IMessageService
{
    private readonly AppDbContext _dbContext;

    public MessageService(AppDbContext dbContext)
    {
        this._dbContext = dbContext;
    }
    public async Task<IEnumerable<Message>> GetGroupMessagesAsync(int groupId)
    {
        var result = await _dbContext.Messages.Where(x => x.GroupChatId == groupId).ToListAsync();
        return result;
    }
    // ikkita tomon id si kiritilishi mumkin methodga
    public async Task<IEnumerable<Message>> GetMessagesByUserIdAsync(int userId)
    {
        var result = await _dbContext.Messages.Where(m => m.SenderId == userId || m.ReceiverId == userId).ToListAsync();
        return result;
    }

    public async Task<Message> SendMessageAsync(CreateMessageDto messageDto)
    {
        Message message = new Message();
        if (messageDto.GroupId == 0)
        {
            message.SenderId = messageDto.SenderId;
            message.ReceiverId = messageDto.ReceiverId;
            message.MessageContent = messageDto.MessageContent;
            message.SentAt = DateTime.UtcNow.AddHours(5);
            message.IsRead = true;
        }
        else if(messageDto.ReceiverId == 0) 
        {
            message.SenderId = messageDto.SenderId;
            message.GroupChatId = messageDto.GroupId;
            message.MessageContent = messageDto.MessageContent;
            message.SentAt = DateTime.UtcNow.AddHours(5);
            message.IsRead = true;
        }
        
        _dbContext.Messages.Add(message);
        await _dbContext.SaveChangesAsync();
        return message;
    }
}
