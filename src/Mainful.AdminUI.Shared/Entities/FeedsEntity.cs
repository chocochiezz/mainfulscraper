	
using System;

namespace Mainful.AdminUI.Shared.Entities
{
	public class FeedsEntity
	{
		public long ID { get; set;}
		public string FeedChannel { get; set;}
		public string Content { get; set;}
		public string ImgUrl { get; set;}
		public string TargetTags { get; set;}
		public DateTime? CreatedDate { get; set;}
		public DateTime? PushDate { get; set;}
		public double PriorityWeight { get; set;}
		public DateTime PlanPushDate { get; set;}
		public string TrackingID { get; set;}
	}
}