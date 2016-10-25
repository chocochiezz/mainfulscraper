
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
	public class ParkingSpacePriceDA : BaseDA
	{
		public ParkingSpacePriceEntity Create(ParkingSpacePriceEntity parkingspacepriceEntity)
		{
			var query = @"INSERT INTO ""ParkingSpacePrice""(""ParkingSpaceID"",""StartDate"",""EndDate"",""Price"",""Category"",""CreatedDate"") VALUES(@ParkingSpaceID,@StartDate,@EndDate,@Price,@Category,@CreatedDate) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, parkingspacepriceEntity).Single();
			parkingspacepriceEntity.ID = id;
			return parkingspacepriceEntity;
		}

		public IEnumerable<ParkingSpacePriceEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""ParkingSpaceID"",""StartDate"",""EndDate"",""Price"",""Category"",""CreatedDate"",""ModifiedDate"" FROM ""ParkingSpacePrice"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var parkingspacepriceEntity = DbConnection.Query<ParkingSpacePriceEntity>(query);
		
			return parkingspacepriceEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""ParkingSpacePrice"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public ParkingSpacePriceEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""ParkingSpacePrice"" WHERE ""ID""=@ID";
		
			var parkingspacepriceEntity = DbConnection.Query<ParkingSpacePriceEntity>(query, new {ID=id}).SingleOrDefault();
		
			return parkingspacepriceEntity;
		}
		
		public int Update(ParkingSpacePriceEntity parkingspacepriceEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<ParkingSpacePriceEntity>(parkingspacepriceEntity) == false)
			{
				var query = @"UPDATE ""ParkingSpacePrice"" SET ""ParkingSpaceID""=@ParkingSpaceID,""StartDate""=@StartDate,""EndDate""=@EndDate,""Price""=@Price,""Category""=@Category,""ModifiedDate""=@ModifiedDate WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, parkingspacepriceEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int ids)
		{
			var query = @"DELETE FROM ""ParkingSpacePrice"" WHERE ""ID"" = @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}