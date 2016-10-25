
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
	public class UserContentBlockedDA : BaseDA
	{
		public UserContentBlockedEntity Create(UserContentBlockedEntity usercontentblockedEntity)
		{
			var query = @"INSERT INTO ""UserContentBlocked""(""SourceName"",""ReferenceID"",""UserProfileID"") VALUES(@SourceName,@ReferenceID,@UserProfileID) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, usercontentblockedEntity).Single();
			usercontentblockedEntity.ID = id;
			return usercontentblockedEntity;
		}

		public IEnumerable<UserContentBlockedEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""SourceName"",""ReferenceID"",""UserProfileID"" FROM ""UserContentBlocked"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var usercontentblockedEntity = DbConnection.Query<UserContentBlockedEntity>(query);
		
			return usercontentblockedEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""UserContentBlocked"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public UserContentBlockedEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""UserContentBlocked"" WHERE ""ID""=@ID";
		
			var usercontentblockedEntity = DbConnection.Query<UserContentBlockedEntity>(query, new {ID=id}).SingleOrDefault();
		
			return usercontentblockedEntity;
		}
		
		public int Update(UserContentBlockedEntity usercontentblockedEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<UserContentBlockedEntity>(usercontentblockedEntity) == false)
			{
				var query = @"UPDATE ""UserContentBlocked"" SET ""SourceName""=@SourceName,""ReferenceID""=@ReferenceID,""UserProfileID""=@UserProfileID WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, usercontentblockedEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int ids)
		{
			var query = @"DELETE FROM  ""UserContentBlocked"" WHERE ""ID"" = @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}