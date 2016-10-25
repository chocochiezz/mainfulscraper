	
using System;

namespace Mainful.AdminUI.Shared.Entities
{
	public class PlaceLocationEntity
	{
		public long ID { get; set;}
		public string PlaceName { get; set;}
		public string Description { get; set;}
		public string PlaceNote { get; set;}		
		public bool IsVenue { get; set;}
		public string Address1 { get; set;}
		public string Address2 { get; set;}
		public string City { get; set;}
		public string State { get; set;}
		public string Country { get; set;}
		public string ZipPostal { get; set;}
		public double Latitude { get; set;}
		public double Longitude { get; set;}
		public double Priority { get; set;}
		public DateTime? CreatedDate { get; set;}
		public DateTime? ModifiedDate { get; set;}
		public int PlaceCategoryID { get; set;}
		public string Phone { get; set;}
		public string Weblink { get; set;}
		public string Email { get; set;}
		public string Slug { get; set;}
		public int SubCategoryID { get; set;}
        public bool HasParking { get; set; }
    }
}