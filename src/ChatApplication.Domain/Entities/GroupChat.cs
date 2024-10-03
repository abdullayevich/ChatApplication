using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ChatApplication.Domain.Entities;

public class GroupChat
{
    public int Id { get; set; }
    public string GroupName { get; set; }
    public int CreatedById { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public User CreatedBy { get; set; }
    public ICollection<GroupMember> GroupMembers { get; set; }
    public ICollection<Message> Messages { get; set; }
}
