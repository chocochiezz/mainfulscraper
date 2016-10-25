
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
	public class CityDA : BaseDA
	{
		public CityEntity Create(CityEntity cityEntity)
		{
			var query = @"INSERT INTO ""City""(""ContinentCode"",""ContinentName"",""CountryIsoCode"",""CountryName"",""Subdivision_1_IsoCode"",""Subdivision_1_Name"",""Subdivision_2_IsoCode"",""Subdivision_2_Name"",""CityName"",""MetroCode"",""TimeZone"") VALUES(@ContinentCode,@ContinentName,@CountryIsoCode,@CountryName,@Subdivision_1_IsoCode,@Subdivision_1_Name,@Subdivision_2_IsoCode,@Subdivision_2_Name,@CityName,@MetroCode,@TimeZone) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, cityEntity).Single();
			cityEntity.ID = id;
			return cityEntity;
		}

		public IEnumerable<CityEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""ContinentCode"",""ContinentName"",""CountryIsoCode"",""CountryName"",""Subdivision_1_IsoCode"",""Subdivision_1_Name"",""Subdivision_2_IsoCode"",""Subdivision_2_Name"",""CityName"",""MetroCode"",""TimeZone"" FROM ""City"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var cityEntity = DbConnection.Query<CityEntity>(query);
		
			return cityEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""City"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public CityEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""City"" WHERE ""ID""=@ID";
		
			var cityEntity = DbConnection.Query<CityEntity>(query, new {ID=id}).SingleOrDefault();
		
			return cityEntity;
		}
		
		public int Update(CityEntity cityEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<CityEntity>(cityEntity) == false)
			{
				var query = @"UPDATE ""City"" SET ""ContinentCode""=@ContinentCode,""ContinentName""=@ContinentName,""CountryIsoCode""=@CountryIsoCode,""CountryName""=@CountryName,""Subdivision_1_IsoCode""=@Subdivision_1_IsoCode,""Subdivision_1_Name""=@Subdivision_1_Name,""Subdivision_2_IsoCode""=@Subdivision_2_IsoCode,""Subdivision_2_Name""=@Subdivision_2_Name,""CityName""=@CityName,""MetroCode""=@MetroCode,""TimeZone""=@TimeZone WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, cityEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int[] ids)
		{
			var query = @"DELETE FROM ""City"" WHERE ""ID"" IN @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}