	
using System;

namespace Mainful.AdminUI.Shared.Entities
{
	public class AppMessageEntity
	{
		public int ID { get; set;}
		public string CriteriaCity { get; set;}
		public string CriteriaGender { get; set;}
		public int CriteriaAgeMin { get; set;}
		public int CriteriaAgeMax { get; set;}
		public string CriteriaDeviceOS { get; set;}
		public string CriteriaDeviceBrand { get; set;}
		public bool CriteriaMember { get; set;}
		public string Content { get; set;}
		public double Weight { get; set;}
		public DateTime StartDate { get; set;}
		public DateTime EndDate { get; set;}
	}
}