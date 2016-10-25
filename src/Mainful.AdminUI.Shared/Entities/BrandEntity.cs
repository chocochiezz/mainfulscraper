	
using System;

namespace Mainful.AdminUI.Shared.Entities
{
	public class BrandEntity
	{
		public int ID { get; set;}
		public string BrandName { get; set;}
		public string Description { get; set;}
		public string Weblink { get; set;}
		public string Facebook { get; set;}
		public string Twitter { get; set;}
		public string GooglePlus { get; set;}
		public string Email { get; set;}
		public string Phone { get; set;}
		public string Instagram { get; set;}
		public byte[] Logo { get; set;}
		public DateTime? ModifiedDate { get; set;}
		public string LogoChecksum { get; set;}
	}
}