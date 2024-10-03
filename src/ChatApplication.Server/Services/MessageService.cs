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
        var result = await _dbContext.Messages.Where(x => x.GroupId == groupId).Include(x => x.GroupChat).ToListAsync();
        return result;
    }
    // ikkita tomon id si kiritilishi mumkin methodga
    public async Task<IEnumerable<Message>> GetMessagesByUserIdAsync(int userId)
    {
        var result = await _dbContext.Messages.Where(m => m.SenderId == userId || m.ReceiverId == userId).ToListAsync();
        throw new NotImplementedException();
    }

    public async Task<Message> SendMessageAsync(CreateMessageDto messageDto)
    {
        Message message = new Message()
        {
            SenderId = messageDto.SenderId,
            ReceiverId = messageDto.ReceiverId,
            GroupId = messageDto.GroupId,
            MessageContent = messageDto.MessageContent,
            SentAt = DateTime.UtcNow.AddHours(5),
        };
        _dbContext.Messages.Add(message);
        await _dbContext.SaveChangesAsync();
        return message;
    }
}
