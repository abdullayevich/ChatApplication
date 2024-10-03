using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Domain.Entities;

public class Message
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public int? ReceiverId { get; set; }  // For personal messages
    public int? GroupChatId { get; set; }     // For group messages
    public string MessageContent { get; set; } = string.Empty;
    public DateTime SentAt { get; set; } = DateTime.Now;
    public bool IsRead { get; set; } = false;

    public User Sender { get; set; }
    public User Receiver { get; set; }
    public GroupChat GroupChat { get; set; }
}
