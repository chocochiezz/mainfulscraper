using System;

namespace Mainful.AdminUI.Shared.Entities
{
    public class LoginEntity
    {
        public string UserName { get; set; }
        public string GroupName { get; set; }
        public int UserID { get; set; }
        public int GroupID { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
