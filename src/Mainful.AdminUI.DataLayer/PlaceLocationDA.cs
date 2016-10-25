
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
	public class PlaceLocationDA : BaseDA
	{
		public PlaceLocationEntity Create(PlaceLocationEntity placelocationEntity)
		{
			var query = @"INSERT INTO ""PlaceLocation""(""PlaceName"",""Description"",""PlaceNote"",""IsVenue"",""Address1"",""Address2"",""City"",""State"",""Country"",""ZipPostal"",""Latitude"",""Longitude"",""Priority"",""CreatedDate"",""PlaceCategoryID"",""Phone"",""Weblink"",""Email"",""Slug"",""SubCategoryID"") VALUES(@PlaceName,@Description,@PlaceNote,@IsVenue,@Address1,@Address2,@City,@State,@Country,@ZipPostal,@Latitude,@Longitude,@Priority,@CreatedDate,@PlaceCategoryID,@Phone,@Weblink,@Email,@Slug,@SubCategoryID) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, placelocationEntity).Single();
			placelocationEntity.ID = id;
			return placelocationEntity;
		}

		public IEnumerable<PlaceLocationEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""PlaceName"",""Description"",""PlaceNote"",""IsVenue"",""Address1"",""Address2"",""City"",""State"",""Country"",""ZipPostal"",""Latitude"",""Longitude"",""Priority"",""CreatedDate"",""ModifiedDate"",""PlaceCategoryID"",""Phone"",""Weblink"",""Email"",""Slug"",""SubCategoryID"" FROM ""PlaceLocation"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var placelocationEntity = DbConnection.Query<PlaceLocationEntity>(query);
		
			return placelocationEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""PlaceLocation"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public PlaceLocationEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""PlaceLocation"" WHERE ""ID""=@ID";
		
			var placelocationEntity = DbConnection.Query<PlaceLocationEntity>(query, new {ID=id}).SingleOrDefault();
		
			return placelocationEntity;
		}
		
		public int Update(PlaceLocationEntity placelocationEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<PlaceLocationEntity>(placelocationEntity) == false)
			{
				var query = @"UPDATE ""PlaceLocation"" SET ""PlaceName""=@PlaceName,""Description""=@Description,""PlaceNote""=@PlaceNote,""IsVenue""=@IsVenue,""Address1""=@Address1,""Address2""=@Address2,""City""=@City,""State""=@State,""Country""=@Country,""ZipPostal""=@ZipPostal,""Latitude""=@Latitude,""Longitude""=@Longitude,""Priority""=@Priority,""ModifiedDate""=@ModifiedDate,""PlaceCategoryID""=@PlaceCategoryID,""Phone""=@Phone,""Weblink""=@Weblink,""Email""=@Email,""Slug""=@Slug,""SubCategoryID""=@SubCategoryID WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, placelocationEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int ids)
		{
			var query = @"DELETE FROM  ""PlaceLocation"" WHERE ""ID"" = @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}