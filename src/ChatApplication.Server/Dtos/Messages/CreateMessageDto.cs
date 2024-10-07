using ChatApplication.Domain.Entities;

namespace ChatApplication.Service.Dtos.Messages
{
    public class CreateMessageDto
    {
        public int SenderId { get; set; }
        public int? ReceiverId { get; set; }
        public string? GroupName { get; set; }
        public string? MessageContent { get; set; }
    }
}
