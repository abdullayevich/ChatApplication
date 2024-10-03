using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Domain.Entities;

public class User
{
    public int Id { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Email { get; set; }
    public string Password { get; set; }
    public bool Status { get; set; } // online = 1, offline = 0, etc.
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public ICollection<Message> SentMessages { get; set; }
    public ICollection<Message> ReceivedMessages { get; set; }
    public ICollection<GroupMember> GroupMemberships { get; set; }
}
