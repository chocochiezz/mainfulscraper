
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
	public class UserInterestDA : BaseDA
	{
		public UserInterestEntity Create(UserInterestEntity userinterestEntity)
		{
			var query = @"INSERT INTO ""UserInterest""(""ContentType"",""ReferenceID"",""UserProfileID"") VALUES(@ContentType,@ReferenceID,@UserProfileID) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, userinterestEntity).Single();
			userinterestEntity.ID = id;
			return userinterestEntity;
		}

		public IEnumerable<UserInterestEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""ContentType"",""ReferenceID"",""UserProfileID"" FROM ""UserInterest"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var userinterestEntity = DbConnection.Query<UserInterestEntity>(query);
		
			return userinterestEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""UserInterest"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public UserInterestEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""UserInterest"" WHERE ""ID""=@ID";
		
			var userinterestEntity = DbConnection.Query<UserInterestEntity>(query, new {ID=id}).SingleOrDefault();
		
			return userinterestEntity;
		}
		
		public int Update(UserInterestEntity userinterestEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<UserInterestEntity>(userinterestEntity) == false)
			{
				var query = @"UPDATE ""UserInterest"" SET ""ContentType""=@ContentType,""ReferenceID""=@ReferenceID,""UserProfileID""=@UserProfileID WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, userinterestEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int ids)
		{
			var query = @"DELETE FROM  ""UserInterest"" WHERE ""ID"" = @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}