using ChatApplication.Domain.Entities;
using ChatApplication.Service.Dtos.Groups;

namespace ChatApplication.Service.Interfaces;

public interface IGroupChatService
{
    public Task<GroupChat> CreateGroupChatAsync(CreateGroupDto groupDto);
    public Task<GroupChat> GetGroupChatByIdAsync(int id);
    public Task<IEnumerable<GroupChat>> GetAllGroupChatsAsync();
    public Task<bool> AddUserToGroupAsync(int groupId, int userId);
}
