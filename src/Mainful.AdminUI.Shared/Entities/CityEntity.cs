	
using System;

namespace Mainful.AdminUI.Shared.Entities
{
	public class CityEntity
	{
		public int ID { get; set;}
		public string ContinentCode { get; set;}
		public string ContinentName { get; set;}
		public string CountryIsoCode { get; set;}
		public string CountryName { get; set;}
		public string Subdivision_1_IsoCode { get; set;}
		public string Subdivision_1_Name { get; set;}
		public string Subdivision_2_IsoCode { get; set;}
		public string Subdivision_2_Name { get; set;}
		public string CityName { get; set;}
		public string MetroCode { get; set;}
		public string TimeZone { get; set;}
	}
}