	
using System;

namespace Mainful.AdminUI.Shared.Entities
{
	public class PlaceUserImageEntity
	{
		public long ID { get; set;}
		public string Content { get; set;}
		public bool IsMain { get; set;}
		public long PlaceID { get; set;}
		public TimeSpan CreatedDate { get; set;}
		public string Checksum { get; set;}
		public string Caption { get; set;}
		public bool UserContent { get; set;}
		public string ContentType { get; set;}
		public bool Approved { get; set;}
		public int Rating { get; set;}
		public long UserID { get; set;}
		public int Point { get; set;}
	}
}