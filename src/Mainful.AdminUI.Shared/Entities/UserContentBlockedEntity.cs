	
using System;

namespace Mainful.AdminUI.Shared.Entities
{
	public class UserContentBlockedEntity
	{
		public long ID { get; set;}
		public string SourceName { get; set;}
		public long ReferenceID { get; set;}
		public long UserProfileID { get; set;}
	}
}