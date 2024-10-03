using ChatApplication.Domain.Entities;

namespace ChatApplication.Service.Interfaces;
public interface IGroupMemberService
{
    public Task<bool> AddUserToGroupAsync(int groupId, int userId);
    public Task<IEnumerable<GroupChat>> GetAllGroupByUserId(int userId);
    public Task<IEnumerable<GroupMember>> GetGroupMembersAsync(int groupId);
    public Task<bool> IsUserInGroupAsync(int groupId, int userId);
}
