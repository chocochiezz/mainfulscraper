
using Mainful.AdminUI.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Dapper;

namespace Mainful.AdminUI.DataLayer
{
    public class LoginDA : BaseDA
    {
        public IEnumerable<LoginEntity> Get(LoginEntity loginEntity)
        {
            var query = @"SELECT ua.""ID"" ""UserID"",ua.""UserName"",ga.""ID"" ""GroupID"",ga.""GroupName"" FROM ""UserAdministrator"" ua 
                        INNER JOIN ""GroupAdministrator"" ga ON ua.""GroupID"" = ga.""ID"" 
                        WHERE ua.""UserName""=@UserName AND ua.""Password""=@Password";

            var groupadministratorEntity = DbConnection.Query<LoginEntity>(query, loginEntity);

            return groupadministratorEntity;
        }
    }
}
