	
using System;

namespace Mainful.AdminUI.Shared.Entities
{
	public class PlaceCategoryEntity
	{
		public int ID { get; set;}
		public string CategoryName { get; set;}
		public string Description { get; set;}
		public byte[] Logo { get; set;}
		public DateTime? ModifiedDate { get; set;}
		public string Tag { get; set;}
		public bool IsPremium { get; set;}
	}
}