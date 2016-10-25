	
using System;

namespace Mainful.AdminUI.Shared.Entities
{
	public class CountryEntity
	{
		public int ID { get; set;}
		public string CountryName { get; set;}
		public string IsoCode { get; set;}
		public string ContinentName { get; set;}
		public string ContinentCode { get; set;}
		public int GeonameID { get; set;}
	}
}