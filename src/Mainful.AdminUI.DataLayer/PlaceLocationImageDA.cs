
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
	public class PlaceLocationImageDA : BaseDA
	{
		public PlaceLocationImageEntity Create(PlaceLocationImageEntity placelocationimageEntity)
		{
			var query = @"INSERT INTO ""PlaceLocationImage""(""Content"",""IsMain"",""ModifiedDate"",""PlaceLocationID"",""Checksum"") VALUES(@Content,@IsMain,@ModifiedDate,@PlaceLocationID,@Checksum) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, placelocationimageEntity).Single();
			placelocationimageEntity.ID = id;
			return placelocationimageEntity;
		}

		public IEnumerable<PlaceLocationImageEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""Content"",""IsMain"",""ModifiedDate"",""PlaceLocationID"",""Checksum"" FROM ""PlaceLocationImage"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var placelocationimageEntity = DbConnection.Query<PlaceLocationImageEntity>(query);
		
			return placelocationimageEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""PlaceLocationImage"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public PlaceLocationImageEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""PlaceLocationImage"" WHERE ""ID""=@ID";
		
			var placelocationimageEntity = DbConnection.Query<PlaceLocationImageEntity>(query, new {ID=id}).SingleOrDefault();
		
			return placelocationimageEntity;
		}
		
		public int Update(PlaceLocationImageEntity placelocationimageEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<PlaceLocationImageEntity>(placelocationimageEntity) == false)
			{
				var query = @"UPDATE ""PlaceLocationImage"" SET ""Content""=@Content,""IsMain""=@IsMain,""ModifiedDate""=@ModifiedDate,""PlaceLocationID""=@PlaceLocationID,""Checksum""=@Checksum WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, placelocationimageEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int ids)
		{
			var query = @"DELETE FROM  ""PlaceLocationImage"" WHERE ""ID"" = @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}