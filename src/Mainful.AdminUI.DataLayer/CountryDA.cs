
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
	public class CountryDA : BaseDA
	{
		public CountryEntity Create(CountryEntity countryEntity)
		{
			var query = @"INSERT INTO ""Country""(""CountryName"",""IsoCode"",""ContinentName"",""ContinentCode"",""GeonameID"") VALUES(@CountryName,@IsoCode,@ContinentName,@ContinentCode,@GeonameID) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, countryEntity).Single();
			countryEntity.ID = id;
			return countryEntity;
		}

		public IEnumerable<CountryEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""CountryName"",""IsoCode"",""ContinentName"",""ContinentCode"",""GeonameID"" FROM ""Country"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var countryEntity = DbConnection.Query<CountryEntity>(query);
		
			return countryEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""Country"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public CountryEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""Country"" WHERE ""ID""=@ID";
		
			var countryEntity = DbConnection.Query<CountryEntity>(query, new {ID=id}).SingleOrDefault();
		
			return countryEntity;
		}
		
		public int Update(CountryEntity countryEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<CountryEntity>(countryEntity) == false)
			{
				var query = @"UPDATE ""Country"" SET ""CountryName""=@CountryName,""IsoCode""=@IsoCode,""ContinentName""=@ContinentName,""ContinentCode""=@ContinentCode,""GeonameID""=@GeonameID WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, countryEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int[] ids)
		{
			var query = @"DELETE FROM ""Country"" WHERE ""ID"" IN @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}