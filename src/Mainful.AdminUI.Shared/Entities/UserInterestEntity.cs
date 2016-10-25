	
using System;

namespace Mainful.AdminUI.Shared.Entities
{
	public class UserInterestEntity
	{
		public long ID { get; set;}
		public string ContentType { get; set;}
		public long ReferenceID { get; set;}
		public long UserProfileID { get; set;}
	}
}