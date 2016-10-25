	
using System;

namespace Mainful.AdminUI.Shared.Entities
{
	public class ParkingSpacePriceEntity
	{
		public long ID { get; set;}
		public long ParkingSpaceID { get; set;}
		public DateTime? StartDate { get; set;}
        public DateTime? EndDate { get; set; }
        public double Price { get; set;}
		public string Category { get; set;}
		public DateTime? CreatedDate { get; set;}
		public DateTime? ModifiedDate { get; set;}
	}
}