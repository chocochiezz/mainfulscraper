	
using System;

namespace Mainful.AdminUI.Shared.Entities
{
	public class RatingEntity
	{
		public long ID { get; set;}
		public string ContentType { get; set;}
		public long ContentID { get; set;}
		public long UserProfileID { get; set;}
		public int Rate { get; set;}
		public string Review { get; set;}
		public bool Like { get; set;}
		public DateTime CreatedDate { get; set;}
		public DateTime? ModifiedDate { get; set;}
	}
}