	
using System;

namespace Mainful.AdminUI.Shared.Entities
{
	public class ParkingSpaceEntity
	{
		public long ID { get; set;}
        public long PlaceID { get; set; }
        public string PlaceName { get; set; }        
		public int Floor { get; set;}
		public string Spot { get; set;}
		public bool IsActive { get; set;}
		public bool IsVIP { get; set;}
		public double Price { get; set;}
		public DateTime? CreatedDate { get; set;}
		public DateTime? ModifiedDate { get; set;}
		public long ContributorID { get; set;}
	}
}