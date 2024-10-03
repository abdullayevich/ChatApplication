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
    public async Task<GroupMember> AddUserToGroupAsync(int groupId, int userId)
    {
        var group = await _dbContext.GroupChats.FindAsync(groupId);
        var user = await _dbContext.Users.FindAsync(userId);
        if (group == null || user == null) 
        {
            throw new BadHttpRequestException("User not found");
        }
        GroupMember groupMember = new GroupMember()
        {
             GroupId = groupId,
             UserId = userId,
             JoinedAt = DateTime.UtcNow.AddHours(5),
        };

        _dbContext.Add(groupMember);
        await _dbContext.SaveChangesAsync();
        return groupMember;
    }

    public async Task<IEnumerable<GroupMember>> GetAllGroupByUserId(int userId)
    {
        var groups = await _dbContext.GroupMembers.Where(u => u.UserId == userId).Include(x => x.GroupChat).ToListAsync();
        return groups;
    }

    public async Task<IEnumerable<GroupMember>> GetGroupMembersAsync(int groupId)
    {
        var result = await _dbContext.GroupMembers
            .Where(g => g.GroupId == groupId)
            .Include(m => m.User)
            .ToListAsync();
        return result;
    }

    public async Task<bool> IsUserInGroupAsync(int groupId, int userId)
    {
        var result = await _dbContext.GroupMembers
            .AnyAsync(gm => gm.GroupId == groupId && gm.UserId == userId);
        return result;
    }
}
