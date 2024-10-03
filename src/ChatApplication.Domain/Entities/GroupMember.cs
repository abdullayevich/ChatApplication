using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Domain.Entities
{
    public class GroupMember
    {
        public int Id { get; set; }
        public int GroupChatId { get; set; }
        public int UserId { get; set; }
        public DateTime JoinedAt { get; set; } = DateTime.Now;

        public GroupChat GroupChat { get; set; }
        public User User { get; set; }
    }

}
