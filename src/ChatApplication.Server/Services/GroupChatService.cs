using ChatApplication.Domain.Entities;
using ChatApplication.Service.Dtos.Groups;
using ChatApplication.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
namespace ChatApplication.Service.Services;

public class GroupChatService : IGroupChatService
{
    private readonly AppDbContext _dbContext;

    public GroupChatService(AppDbContext dbContext)
    {
        this._dbContext = dbContext;

    }
    public Task<bool> AddUserToGroupAsync(int groupId, int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<GroupChat> CreateGroupChatAsync(CreateGroupDto groupDto)
    {
        GroupChat group = new GroupChat()
        {
            GroupName = groupDto.GroupName,
            CreatedById = groupDto.CreatedById,
            CreatedAt = DateTime.UtcNow.AddHours(5),

        };
        _dbContext.GroupChats.Add(group);
        await _dbContext.SaveChangesAsync();
        return group;
    }

    public async Task<IEnumerable<GroupChat>> GetAllGroupChatsAsync()
    {
        var result = await _dbContext.GroupChats.Include(gc => gc.GroupMembers)
            .Select(x => new GroupChat
            {
                Id = x.Id,
                CreatedAt = x.CreatedAt,
                GroupName = x.GroupName,
                CreatedById = x.CreatedById
            }).ToListAsync();
        return result;
    }

    public async Task<GroupChat> GetGroupChatByIdAsync(int groupId)
    {
        var result = await _dbContext.GroupChats.Include(gm => gm.Messages)
            .Select(x => new GroupChat
            {
                Id = x.Id,
                GroupName = x.GroupName,
                CreatedById = x.CreatedById,
                CreatedAt = x.CreatedAt,
                Messages = x.Messages
                     .Where(z => z.GroupChatId == groupId)
                     .Select(z => new Message
                     {
                         Id = z.Id,
                         SenderId = z.SenderId,
                         GroupChatId = z.GroupChatId,
                         MessageContent = z.MessageContent,
                         SentAt = z.SentAt,
                         ReceiverId = z.ReceiverId,

                     }).ToList()
            }).FirstOrDefaultAsync(gm => gm.Id == groupId);
        return result;

        //var result = await _dbContext.GroupMembers
        //    .Where(g => g.GroupChatId == groupId)
        //    .Include(m => m.User)
        //    .Select(x => new GroupMember
        //    {
        //        Id = x.Id,
        //        UserId = x.UserId,
        //        JoinedAt = x.JoinedAt,
        //        GroupChatId = x.GroupChatId,
        //        User = new User()
        //        {
        //            Id = x.UserId,
        //            Username = x.User.Username,
        //            Email = x.User.Email,
        //            CreatedAt = x.User.CreatedAt,
        //            Status = x.User.Status,
        //        }
        //    })
        //    .ToListAsync();
        //return result;
    }
}
