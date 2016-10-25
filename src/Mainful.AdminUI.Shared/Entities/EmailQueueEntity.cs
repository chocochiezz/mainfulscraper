	
using System;

namespace Mainful.AdminUI.Shared.Entities
{
	public class EmailQueueEntity
	{
		public long ID { get; set;}
		public string Subject { get; set;}
		public string FromAddress { get; set;}
		public string FromName { get; set;}
		public string ToAddress { get; set;}
		public string CcAddress { get; set;}
		public string BccAddress { get; set;}
		public DateTime CreatedDate { get; set;}
		public DateTime? SendDate { get; set;}
		public DateTime? ResendDate { get; set;}
		public string Body { get; set;}
	}
}