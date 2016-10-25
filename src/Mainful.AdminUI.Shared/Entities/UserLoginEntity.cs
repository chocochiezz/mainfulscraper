	
using System;

namespace Mainful.AdminUI.Shared.Entities
{
	public class UserLoginEntity
	{
		public long ID { get; set;}
		public string LoginProvider { get; set;}
		public string ProviderKey { get; set;}
		public long UserProfileID { get; set;}
	}
}