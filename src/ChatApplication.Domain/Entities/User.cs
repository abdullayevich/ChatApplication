using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Status { get; set; } // online, offline, etc.
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Relationships
    public ICollection<Message> SentMessages { get; set; }
    public ICollection<Message> ReceivedMessages { get; set; }
    public ICollection<GroupMember> GroupMemberships { get; set; }
}
