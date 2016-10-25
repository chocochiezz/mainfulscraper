	
using System;

namespace Mainful.AdminUI.Shared.Entities
{
	public class GroupAdministratorEntity
	{
		public long ID { get; set;}
		public string GroupName { get; set;}
		public string Description { get; set;}
		public int CreatedBy { get; set;}
		public int ModifiedBy { get; set;}
		public DateTime? CreatedDate { get; set;}
		public DateTime? ModifiedDate { get; set;}		
		public bool IsDeleted { get; set;}
	}
}