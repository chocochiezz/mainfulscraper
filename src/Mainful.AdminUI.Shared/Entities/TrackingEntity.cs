	
using System;

namespace Mainful.AdminUI.Shared.Entities
{
	public class TrackingEntity
	{
		public long ID { get; set;}
		public string TrackingID { get; set;}
		public DateTime CreatedDate { get; set;}
		public long UserID { get; set;}
		public string DeviceModel { get; set;}
		public string DeviceBrand { get; set;}
		public string DeviceID { get; set;}
		public string UserFullname { get; set;}
		public string UserEmail { get; set;}
		public string Channel { get; set;}
		public string ClientID { get; set;}
		public string TrackingChannel { get; set;}
		public string Params { get; set;}
	}
}