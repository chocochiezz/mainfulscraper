
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
	public class RatingDA : BaseDA
	{
		public RatingEntity Create(RatingEntity ratingEntity)
		{
			var query = @"INSERT INTO ""Rating""(""ContentType"",""ContentID"",""UserProfileID"",""Rate"",""Review"",""Like"",""CreatedDate"",""ModifiedDate"") VALUES(@ContentType,@ContentID,@UserProfileID,@Rate,@Review,@Like,@CreatedDate,@ModifiedDate) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, ratingEntity).Single();
			ratingEntity.ID = id;
			return ratingEntity;
		}

		public IEnumerable<RatingEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""ContentType"",""ContentID"",""UserProfileID"",""Rate"",""Review"",""Like"",""CreatedDate"",""ModifiedDate"" FROM ""Rating"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var ratingEntity = DbConnection.Query<RatingEntity>(query);
		
			return ratingEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""Rating"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public RatingEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""Rating"" WHERE ""ID""=@ID";
		
			var ratingEntity = DbConnection.Query<RatingEntity>(query, new {ID=id}).SingleOrDefault();
		
			return ratingEntity;
		}
		
		public int Update(RatingEntity ratingEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<RatingEntity>(ratingEntity) == false)
			{
				var query = @"UPDATE ""Rating"" SET ""ContentType""=@ContentType,""ContentID""=@ContentID,""UserProfileID""=@UserProfileID,""Rate""=@Rate,""Review""=@Review,""Like""=@Like,""CreatedDate""=@CreatedDate,""ModifiedDate""=@ModifiedDate WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, ratingEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int ids)
		{
			var query = @"DELETE FROM  ""Rating"" WHERE ""ID"" = @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}