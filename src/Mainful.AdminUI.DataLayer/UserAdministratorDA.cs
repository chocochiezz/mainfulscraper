
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
    public class UserAdministratorDA : BaseDA
    {
        public UserAdministratorEntity Create(UserAdministratorEntity useradministratorEntity)
        {
            var query = @"INSERT INTO ""UserAdministrator""(""UserName"",""Password"",""Name"",""Email"",""Phone"",""CreatedDate"",""Picture"",""GroupID"",""CreatedBy"") VALUES(@UserName,@Password,@Name,@Email,@Phone,@CreatedDate,@Picture,@GroupID,@CreatedBy) RETURNING ""ID"";";

            int id = DbConnection.Query<int>(query, useradministratorEntity).Single();
            useradministratorEntity.ID = id;
            return useradministratorEntity;
        }

        public IEnumerable<UserAdministratorEntity> GetAll(DBParamEntity dbParamEntity)
        {
            var query = @"SELECT ""ID"",""UserName"",""Password"",""Name"",""Email"",""Phone"",""CreatedDate"",""ModifiedDate"",""Picture"",""IsLocked"",""IsDeleted"",""PasscodeExpired"",""GroupID"",""CreatedBy"",""ModifiedBy"" FROM ""UserAdministrator"" {{Filter}} {{Sorting}} {{Paging}}";

            query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
            query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
            query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));

            var useradministratorEntity = DbConnection.Query<UserAdministratorEntity>(query);

            return useradministratorEntity;
        }

        public int GetTotalRows(DBParamEntity dbParamEntity)
        {
            var query = @"SELECT COUNT(""ID"") FROM ""UserAdministrator"" {{Filter}}";

            query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));

            return DbConnection.Query<int>(query).Single();
        }

        public UserAdministratorEntity GetById(int id)
        {
            var query = @"SELECT * FROM ""UserAdministrator"" WHERE ""ID""=@ID AND ""IsDeleted""=false";

            var useradministratorEntity = DbConnection.Query<UserAdministratorEntity>(query, new { ID = id }).SingleOrDefault();

            return useradministratorEntity;
        }

        public IEnumerable<UserAdministratorEntity> GetByUserName(string userName)
        {
            var query = @"SELECT * FROM ""UserAdministrator"" WHERE ""UserName""=@UserName AND ""IsDeleted""=false";

            var useradministratorEntity = DbConnection.Query<UserAdministratorEntity>(query, new { UserName = userName });

            return useradministratorEntity;
        }

        public int Update(UserAdministratorEntity useradministratorEntity)
        {
            int affectedRows = 0;
            if (IsHaveId<UserAdministratorEntity>(useradministratorEntity) == false)
            {
                //var query = @"UPDATE ""UserAdministrator"" SET ""UserName""=@UserName,""Password""=@Password,""Name""=@Name,""Email""=@Email,""Phone""=@Phone,""CreatedDate""=@CreatedDate,""ModifiedDate""=@ModifiedDate,""Picture""=@Picture,""IsLocked""=@IsLocked,""IsDeleted""=@IsDeleted,""PasscodeExpired""=@PasscodeExpired,""GroupID""=@GroupID WHERE ""ID""=@ID";
                var query = @"UPDATE ""UserAdministrator"" SET ""UserName""=@UserName,""Password""=@Password,""Name""=@Name,""Email""=@Email,""Phone""=@Phone,""ModifiedDate""=@ModifiedDate,""Picture""=@Picture,""GroupID""=@GroupID,""ModifiedBy""=@ModifiedBy WHERE ""ID""=@ID";
                affectedRows = DbConnection.Execute(query, useradministratorEntity);
            }

            return affectedRows;
        }

        public UserAdministratorEntity CheckPassword(long id, string password)
        {
            var query = @"SELECT * FROM ""UserAdministrator"" WHERE ""ID""=@ID AND ""Password""=@Password";

            var useradministratorEntity = DbConnection.Query<UserAdministratorEntity>(query, new { ID = id, Password = password }).SingleOrDefault();

            return useradministratorEntity;
        }

        public int Delete(int ids)
        {
            var query = @"UPDATE ""UserAdministrator"" SET ""IsDeleted""=true WHERE ""ID""=@ID";
            var affectedRows = DbConnection.Execute(query, new { ID = ids });

            return affectedRows;
        }
    }
}