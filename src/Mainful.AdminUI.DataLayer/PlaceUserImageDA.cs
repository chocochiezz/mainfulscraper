
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
	public class PlaceUserImageDA : BaseDA
	{
		public PlaceUserImageEntity Create(PlaceUserImageEntity placeuserimageEntity)
		{
			var query = @"INSERT INTO ""PlaceUserImage""(""Content"",""IsMain"",""PlaceID"",""CreatedDate"",""Checksum"",""Caption"",""UserContent"",""ContentType"",""Approved"",""Rating"",""UserID"",""Point"") VALUES(@Content,@IsMain,@PlaceID,@CreatedDate,@Checksum,@Caption,@UserContent,@ContentType,@Approved,@Rating,@UserID,@Point) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, placeuserimageEntity).Single();
			placeuserimageEntity.ID = id;
			return placeuserimageEntity;
		}

		public IEnumerable<PlaceUserImageEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""Content"",""IsMain"",""PlaceID"",""CreatedDate"",""Checksum"",""Caption"",""UserContent"",""ContentType"",""Approved"",""Rating"",""UserID"",""Point"" FROM ""PlaceUserImage"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var placeuserimageEntity = DbConnection.Query<PlaceUserImageEntity>(query);
		
			return placeuserimageEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""PlaceUserImage"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public PlaceUserImageEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""PlaceUserImage"" WHERE ""ID""=@ID";
		
			var placeuserimageEntity = DbConnection.Query<PlaceUserImageEntity>(query, new {ID=id}).SingleOrDefault();
		
			return placeuserimageEntity;
		}
		
		public int Update(PlaceUserImageEntity placeuserimageEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<PlaceUserImageEntity>(placeuserimageEntity) == false)
			{
				var query = @"UPDATE ""PlaceUserImage"" SET ""Content""=@Content,""IsMain""=@IsMain,""PlaceID""=@PlaceID,""CreatedDate""=@CreatedDate,""Checksum""=@Checksum,""Caption""=@Caption,""UserContent""=@UserContent,""ContentType""=@ContentType,""Approved""=@Approved,""Rating""=@Rating,""UserID""=@UserID,""Point""=@Point WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, placeuserimageEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int ids)
		{
			var query = @"DELETE FROM  ""PlaceUserImage"" WHERE ""ID"" = @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}