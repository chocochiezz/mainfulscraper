	
using System;

namespace Mainful.AdminUI.Shared.Entities
{
	public class PlaceLocationImageEntity
	{
		public long ID { get; set;}
		public byte[] Content { get; set;}
		public bool IsMain { get; set;}
		public DateTime? ModifiedDate { get; set;}
		public long PlaceLocationID { get; set;}
		public string Checksum { get; set;}
	}
}