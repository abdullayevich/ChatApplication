using ChatApplication.Domain.Entities;

namespace ChatApplication.Service.Dtos.Groups;

public class CreateGroupDto
{
    public string GroupName { get; set; }
    public int CreatedById { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

}
