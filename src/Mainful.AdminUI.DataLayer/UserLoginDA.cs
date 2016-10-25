
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
	public class UserLoginDA : BaseDA
	{
		public UserLoginEntity Create(UserLoginEntity userloginEntity)
		{
			var query = @"INSERT INTO ""UserLogin""(""LoginProvider"",""ProviderKey"",""UserProfileID"") VALUES(@LoginProvider,@ProviderKey,@UserProfileID) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, userloginEntity).Single();
			userloginEntity.ID = id;
			return userloginEntity;
		}

		public IEnumerable<UserLoginEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""LoginProvider"",""ProviderKey"",""UserProfileID"" FROM ""UserLogin"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var userloginEntity = DbConnection.Query<UserLoginEntity>(query);
		
			return userloginEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""UserLogin"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public UserLoginEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""UserLogin"" WHERE ""ID""=@ID";
		
			var userloginEntity = DbConnection.Query<UserLoginEntity>(query, new {ID=id}).SingleOrDefault();
		
			return userloginEntity;
		}
		
		public int Update(UserLoginEntity userloginEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<UserLoginEntity>(userloginEntity) == false)
			{
				var query = @"UPDATE ""UserLogin"" SET ""LoginProvider""=@LoginProvider,""ProviderKey""=@ProviderKey,""UserProfileID""=@UserProfileID WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, userloginEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int ids)
		{
			var query = @"DELETE FROM  ""UserLogin"" WHERE ""ID"" = @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}