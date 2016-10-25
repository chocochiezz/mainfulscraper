	
using System;

namespace Mainful.AdminUI.Shared.Entities
{
	public class ReportContentEntity
	{
		public long ID { get; set;}
		public string Content { get; set;}
		public string Category { get; set;}
		public int Rating { get; set;}
		public string Context { get; set;}
		public int ReffID { get; set;}
		public bool Approved { get; set;}
		public string Comment { get; set;}
		public TimeSpan? CreatedDate { get; set;}
	}
}