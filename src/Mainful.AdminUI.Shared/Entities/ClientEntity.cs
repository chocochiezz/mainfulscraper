	
using System;

namespace Mainful.AdminUI.Shared.Entities
{
	public class ClientEntity
	{
		public int ID { get; set;}
		public string Channel { get; set;}
		public string ClientID { get; set;}
		public string SecretKey { get; set;}
		public DateTime? CreatedDate { get; set;}
		public DateTime? ModifiedDate { get; set;}
	}
}