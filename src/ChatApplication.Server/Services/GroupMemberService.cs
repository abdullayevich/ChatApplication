using ChatApplication.Domain.Entities;
using ChatApplication.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Service.Services;

public class GroupMemberService : IGroupMemberService
{
    private readonly AppDbContext _dbContext;

    public GroupMemberService(AppDbContext dbContext)
    {
        this._dbContext = dbContext;
    }
    public async Task<bool> AddUserToGroupAsync(int groupId, int userId)
    {
        var group = await _dbContext.GroupChats.FindAsync(groupId);
        var user = await _dbContext.Users.FindAsync(userId);
        if (group == null || user == null)
        {
            throw new BadHttpRequestException("User not found");
        }
        GroupMember groupMember = new GroupMember()
        {
            GroupChatId = groupId,
            UserId = userId,
            JoinedAt = DateTime.UtcNow.AddHours(5),
        };

        _dbContext.Add(groupMember);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<IEnumerable<GroupChat>> GetAllGroupByUserId(int userId)
    {
        var groups = await _dbContext.GroupMembers.Where(u => u.UserId == userId).Include(x => x.GroupChat).Select((x) => x.GroupChat).ToListAsync();
        return groups;
    }

    public async Task<IEnumerable<GroupMember>> GetGroupMembersAsync(int groupId)
    {
        var result = await _dbContext.GroupMembers
            .Where(g => g.GroupChatId == groupId)
            .Include(m => m.User).Select(x => new GroupMember
            {
                Id = x.Id,
                UserId = x.UserId,
                JoinedAt = x.JoinedAt,
                GroupChatId = x.GroupChatId,
                User = new User()
                {
                    Id = x.UserId,
                    Username = x.User.Username,
                    Email = x.User.Email,
                    CreatedAt = x.User.CreatedAt,
                    Status = x.User.Status,
                }
            })
            .ToListAsync();
        return result;
    }

    public async Task<bool> IsUserInGroupAsync(int groupId, int userId)
    {
        var result = await _dbContext.GroupMembers
            .AnyAsync(gm => gm.GroupChatId == groupId && gm.UserId == userId);
        return result;
    }
}
