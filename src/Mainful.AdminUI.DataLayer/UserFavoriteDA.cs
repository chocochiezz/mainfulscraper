
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
	public class UserFavoriteDA : BaseDA
	{
		public UserFavoriteEntity Create(UserFavoriteEntity userfavoriteEntity)
		{
			var query = @"INSERT INTO ""UserFavorite""(""ContentType"",""ReferenceID"",""UserProfileID"") VALUES(@ContentType,@ReferenceID,@UserProfileID) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, userfavoriteEntity).Single();
			userfavoriteEntity.ID = id;
			return userfavoriteEntity;
		}

		public IEnumerable<UserFavoriteEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""ContentType"",""ReferenceID"",""UserProfileID"" FROM ""UserFavorite"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var userfavoriteEntity = DbConnection.Query<UserFavoriteEntity>(query);
		
			return userfavoriteEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""UserFavorite"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public UserFavoriteEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""UserFavorite"" WHERE ""ID""=@ID";
		
			var userfavoriteEntity = DbConnection.Query<UserFavoriteEntity>(query, new {ID=id}).SingleOrDefault();
		
			return userfavoriteEntity;
		}
		
		public int Update(UserFavoriteEntity userfavoriteEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<UserFavoriteEntity>(userfavoriteEntity) == false)
			{
				var query = @"UPDATE ""UserFavorite"" SET ""ContentType""=@ContentType,""ReferenceID""=@ReferenceID,""UserProfileID""=@UserProfileID WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, userfavoriteEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int ids)
		{
			var query = @"DELETE FROM  ""UserFavorite"" WHERE ""ID"" = @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}