
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
	public class ParkingSpaceDA : BaseDA
	{
		public ParkingSpaceEntity Create(ParkingSpaceEntity parkingspaceEntity)
		{
			var query = @"INSERT INTO ""ParkingSpace""(""PlaceID"",""Floor"",""Spot"",""IsActive"",""IsVIP"",""Price"",""CreatedDate"",""ModifiedDate"",""ContributorID"") VALUES(@PlaceID,@Floor,@Spot,@IsActive,@IsVIP,@Price,@CreatedDate,@ModifiedDate,@ContributorID) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, parkingspaceEntity).Single();
			parkingspaceEntity.ID = id;
			return parkingspaceEntity;
		}

		public IEnumerable<ParkingSpaceEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"  SELECT a.*,b.""PlaceName"" FROM ""ParkingSpace"" a
                            INNER JOIN ""PlaceLocation"" b ON a.""PlaceID"" = b.""ID""
                            {{Filter}} {{Sorting}} {{Paging}}";

            query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var parkingspaceEntity = DbConnection.Query<ParkingSpaceEntity>(query);
		
			return parkingspaceEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""ParkingSpace"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public ParkingSpaceEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""ParkingSpace"" WHERE ""ID""=@ID";
		
			var parkingspaceEntity = DbConnection.Query<ParkingSpaceEntity>(query, new {ID=id}).SingleOrDefault();
		
			return parkingspaceEntity;
		}
		
		public int Update(ParkingSpaceEntity parkingspaceEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<ParkingSpaceEntity>(parkingspaceEntity) == false)
			{
				var query = @"UPDATE ""ParkingSpace"" SET ""PlaceID""=@PlaceID,""Floor""=@Floor,""Spot""=@Spot,""IsActive""=@IsActive,""IsVIP""=@IsVIP,""Price""=@Price,""CreatedDate""=@CreatedDate,""ModifiedDate""=@ModifiedDate,""ContributorID""=@ContributorID WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, parkingspaceEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int[] ids)
		{
			var query = @"DELETE FROM ""ParkingSpace"" WHERE ""ID"" IN @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}