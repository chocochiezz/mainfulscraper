
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
	public class PlaceSubCategoryDA : BaseDA
	{
		public PlaceSubCategoryEntity Create(PlaceSubCategoryEntity placesubcategoryEntity)
		{
			var query = @"INSERT INTO ""PlaceSubCategory""(""CategoryName"",""Description"",""Logo"",""ModifiedDate"",""Tag"",""IsPremium"",""ParentID"") VALUES(@CategoryName,@Description,@Logo,@ModifiedDate,@Tag,@IsPremium,@ParentID) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, placesubcategoryEntity).Single();
			placesubcategoryEntity.ID = id;
			return placesubcategoryEntity;
		}

		public IEnumerable<PlaceSubCategoryEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""CategoryName"",""Description"",""Logo"",""ModifiedDate"",""Tag"",""IsPremium"",""ParentID"" FROM ""PlaceSubCategory"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var placesubcategoryEntity = DbConnection.Query<PlaceSubCategoryEntity>(query);
		
			return placesubcategoryEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""PlaceSubCategory"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public PlaceSubCategoryEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""PlaceSubCategory"" WHERE ""ID""=@ID";
		
			var placesubcategoryEntity = DbConnection.Query<PlaceSubCategoryEntity>(query, new {ID=id}).SingleOrDefault();
		
			return placesubcategoryEntity;
		}
		
		public int Update(PlaceSubCategoryEntity placesubcategoryEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<PlaceSubCategoryEntity>(placesubcategoryEntity) == false)
			{
				var query = @"UPDATE ""PlaceSubCategory"" SET ""CategoryName""=@CategoryName,""Description""=@Description,""Logo""=@Logo,""ModifiedDate""=@ModifiedDate,""Tag""=@Tag,""IsPremium""=@IsPremium,""ParentID""=@ParentID WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, placesubcategoryEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int ids)
		{
			var query = @"DELETE FROM  ""PlaceSubCategory"" WHERE ""ID"" = @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}