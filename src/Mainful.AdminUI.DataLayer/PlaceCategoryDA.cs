
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
	public class PlaceCategoryDA : BaseDA
	{
		public PlaceCategoryEntity Create(PlaceCategoryEntity placecategoryEntity)
		{
			var query = @"INSERT INTO ""PlaceCategory""(""CategoryName"",""Description"",""Logo"",""ModifiedDate"",""Tag"",""IsPremium"") VALUES(@CategoryName,@Description,@Logo,@ModifiedDate,@Tag,@IsPremium) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, placecategoryEntity).Single();
			placecategoryEntity.ID = id;
			return placecategoryEntity;
		}

		public IEnumerable<PlaceCategoryEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""CategoryName"",""Description"",""Logo"",""ModifiedDate"",""Tag"",""IsPremium"" FROM ""PlaceCategory"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var placecategoryEntity = DbConnection.Query<PlaceCategoryEntity>(query);
		
			return placecategoryEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""PlaceCategory"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public PlaceCategoryEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""PlaceCategory"" WHERE ""ID""=@ID";
		
			var placecategoryEntity = DbConnection.Query<PlaceCategoryEntity>(query, new {ID=id}).SingleOrDefault();
		
			return placecategoryEntity;
		}
		
		public int Update(PlaceCategoryEntity placecategoryEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<PlaceCategoryEntity>(placecategoryEntity) == false)
			{
				var query = @"UPDATE ""PlaceCategory"" SET ""CategoryName""=@CategoryName,""Description""=@Description,""Logo""=@Logo,""ModifiedDate""=@ModifiedDate,""Tag""=@Tag,""IsPremium""=@IsPremium WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, placecategoryEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int ids)
		{
			var query = @"DELETE FROM  ""PlaceCategory"" WHERE ""ID"" = @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}