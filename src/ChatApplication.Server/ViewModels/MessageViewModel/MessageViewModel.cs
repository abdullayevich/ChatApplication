using System.ComponentModel.DataAnnotations;

namespace ChatApplication.Service.ViewModels.MessageViewModel
{
	public class MessageViewModel
	{
		public int Id { get; set; }
		[Required]
		public string MessageContent { get; set; }
		public DateTime Timestamp { get; set; }
		public string FromUserName { get; set; }
		[Required]
		public string Chat { get; set; }
	}
}
