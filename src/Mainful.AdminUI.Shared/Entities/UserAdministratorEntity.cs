	
using System;

namespace Mainful.AdminUI.Shared.Entities
{
	public class UserAdministratorEntity
	{
		public long ID { get; set;}
		public string UserName { get; set;}
		public string Password { get; set;}
		public string Name { get; set;}
		public string Email { get; set;}
		public string Phone { get; set;}
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set;}
		public DateTime? ModifiedDate { get; set;}
		public string Picture { get; set;}
		public bool? IsLocked { get; set;}
		public bool? IsDeleted { get; set;}
		public DateTime? PasscodeExpired { get; set;}
		public long GroupID { get; set;}
	}
}