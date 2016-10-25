	
using System;

namespace Mainful.AdminUI.Shared.Entities
{
	public class EventEntity
	{
		public long ID { get; set;}
		public string Title { get; set;}
		public string Description { get; set;}
		public DateTime? StartDate { get; set;}
		public TimeSpan? StartTime { get; set;}
		public DateTime EndDate { get; set;}
		public TimeSpan? EndTime { get; set;}
		public string Timezone { get; set;}
		public string Weblink { get; set;}
		public string Facebook { get; set;}
		public string Twitter { get; set;}
		public string GooglePlus { get; set;}
		public string Email { get; set;}
		public string Online { get; set;}
		public DateTime CreatedDate { get; set;}
		public DateTime? ModifiedDate { get; set;}
		public int EventCategoryID { get; set;}
		public int EventOrganizerID { get; set;}
		public string Tag { get; set;}
		public bool IsFree { get; set;}
		public string Currency { get; set;}
		public string PriceRange { get; set;}
		public double Priority { get; set;}
		public string EventType { get; set;}
		public string Slug { get; set;}
	}
}