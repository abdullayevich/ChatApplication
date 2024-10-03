using ChatApplication.Domain.Entities;
using ChatApplication.Service.Dtos.Groups;
using ChatApplication.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
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
        var result = await _dbContext.GroupChats.Include(gc => gc.GroupMembers).ToListAsync();
        return result;
    }

    public async Task<GroupChat> GetGroupChatByIdAsync(int id)
    {
        var result = await _dbContext.GroupChats.Include(gm => gm.GroupMembers)
            .FirstOrDefaultAsync(gm => gm.Id == id);
        return result;
    }
}
