	
using System;

namespace Mainful.AdminUI.Shared.Entities
{
	public class PromoImageEntity
	{
		public long ID { get; set;}
		public bool IsMain { get; set;}
		public long PromoID { get; set;}
		public byte[] Content { get; set;}
		public DateTime? ModifiedDate { get; set;}
		public string Checksum { get; set;}
	}
}