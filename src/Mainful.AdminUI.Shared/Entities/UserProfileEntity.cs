	
using System;

namespace Mainful.AdminUI.Shared.Entities
{
	public class UserProfileEntity
	{
		public long ID { get; set;}
		public string Email { get; set;}
		public bool EmailConfirmed { get; set;}
		public string Name { get; set;}
		public string Gender { get; set;}
		public DateTime? Birthdate { get; set;}
		public string Phone { get; set;}
		public string PasswordHash { get; set;}
		public DateTime? CreatedDate { get; set;}
		public DateTime? ModifiedDate { get; set;}
		public string ReminderSetting { get; set;}
		public bool PushNotification { get; set;}
		public string AvatarUrl { get; set;}
		public bool IsDeleted { get; set;}
		public string Status { get; set;}
		public string Passcode { get; set;}
		public DateTime? PasscodeExpired { get; set;}
	}
}