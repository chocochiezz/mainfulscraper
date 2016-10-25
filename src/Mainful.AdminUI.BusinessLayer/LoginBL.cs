using System;
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using Mainful.AdminUI.Shared.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Mainful.AdminUI.BusinessLayer
{
    public class LoginBL : BaseBL
    {
        public List<LoginEntity> Get(LoginEntity loginEntity)
        {
            var result = new List<LoginEntity>();

            loginEntity.Password = UtilityHelper.PasswordHash(loginEntity.Password);

            using (var loginDA = new LoginDA())
            {
                result = loginDA.Get(loginEntity).ToList<LoginEntity>();
            }

            return result;
        }
    }
}
