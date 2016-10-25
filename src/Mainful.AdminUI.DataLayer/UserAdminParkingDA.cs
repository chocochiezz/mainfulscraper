
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
	public class UserAdminParkingDA : BaseDA
	{
		public UserAdminParkingEntity Create(UserAdminParkingEntity useradminparkingEntity)
		{
			var query = @"INSERT INTO ""UserAdminParking""(""UserProfileID"",""PlaceLocationID"",""StaffCode"",""CreatedDate"",""IsActive"") VALUES(@UserProfileID,@PlaceLocationID,@StaffCode,@CreatedDate,@IsActive) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, useradminparkingEntity).Single();
			useradminparkingEntity.ID = id;
			return useradminparkingEntity;
		}

		public IEnumerable<UserAdminParkingEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""UserProfileID"",""PlaceLocationID"",""StaffCode"",""CreatedDate"",""IsActive"" FROM ""UserAdminParking"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var useradminparkingEntity = DbConnection.Query<UserAdminParkingEntity>(query);
		
			return useradminparkingEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""UserAdminParking"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public UserAdminParkingEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""UserAdminParking"" WHERE ""ID""=@ID";
		
			var useradminparkingEntity = DbConnection.Query<UserAdminParkingEntity>(query, new {ID=id}).SingleOrDefault();
		
			return useradminparkingEntity;
		}
		
		public int Update(UserAdminParkingEntity useradminparkingEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<UserAdminParkingEntity>(useradminparkingEntity) == false)
			{
				var query = @"UPDATE ""UserAdminParking"" SET ""UserProfileID""=@UserProfileID,""PlaceLocationID""=@PlaceLocationID,""StaffCode""=@StaffCode,""IsActive""=@IsActive WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, useradminparkingEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int ids)
		{
			var query = @"DELETE FROM ""UserAdminParking"" WHERE ""ID"" = @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}