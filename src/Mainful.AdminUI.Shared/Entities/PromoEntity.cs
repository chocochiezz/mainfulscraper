	
using System;

namespace Mainful.AdminUI.Shared.Entities
{
	public class PromoEntity
	{
		public long ID { get; set;}
		public string Title { get; set;}
		public string Description { get; set;}
		public DateTime? StartDate { get; set;}
		public TimeSpan? StartTime { get; set;}
		public DateTime? EndDate { get; set;}
		public TimeSpan? EndTime { get; set;}
		public string Tag { get; set;}
		public string Issuer { get; set;}
		public string Days { get; set;}
		public string Times { get; set;}
		public string Terms { get; set;}
		public string Online { get; set;}
		public DateTime CreatedDate { get; set;}
		public DateTime? ModifiedDate { get; set;}
		public int PromoCategoryID { get; set;}
		public int BrandID { get; set;}
		public double Priority { get; set;}
		public string Slug { get; set;}
	}
}