	
using System;

namespace Mainful.AdminUI.Shared.Entities
{
	public class EventImageEntity
	{
		public long ID { get; set;}
		public bool IsMain { get; set;}
		public long EventID { get; set;}
		public byte[] Content { get; set;}
		public DateTime? ModifiedDate { get; set;}
		public string Checksum { get; set;}
	}
}