	
using System;

namespace Mainful.AdminUI.Shared.Entities
{
	public class EventOrganizerEntity
	{
		public int ID { get; set;}
		public string Name { get; set;}
		public string Description { get; set;}
		public string Phone1 { get; set;}
		public string Phone2 { get; set;}
		public byte[] Logo { get; set;}
		public string ShortName { get; set;}
		public string LongDescription { get; set;}
		public DateTime? ModifiedDate { get; set;}
		public string LogoChecksum { get; set;}
	}
}