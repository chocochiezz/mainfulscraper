	
using System;

namespace Mainful.AdminUI.Shared.Entities
{
	public class UserAdminParkingEntity
	{
		public int ID { get; set;}
		public long UserProfileID { get; set;}
		public long PlaceLocationID { get; set;}
		public string StaffCode { get; set;}
		public DateTime CreatedDate { get; set;}
		public bool IsActive { get; set;}
	}
}