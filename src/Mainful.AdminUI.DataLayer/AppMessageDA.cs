
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
	public class AppMessageDA : BaseDA
	{
		public AppMessageEntity Create(AppMessageEntity appmessageEntity)
		{
			var query = @"INSERT INTO ""AppMessage""(""CriteriaCity"",""CriteriaGender"",""CriteriaAgeMin"",""CriteriaAgeMax"",""CriteriaDeviceOS"",""CriteriaDeviceBrand"",""CriteriaMember"",""Content"",""Weight"",""StartDate"",""EndDate"") VALUES(@CriteriaCity,@CriteriaGender,@CriteriaAgeMin,@CriteriaAgeMax,@CriteriaDeviceOS,@CriteriaDeviceBrand,@CriteriaMember,@Content,@Weight,@StartDate,@EndDate) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, appmessageEntity).Single();
			appmessageEntity.ID = id;
			return appmessageEntity;
		}

		public IEnumerable<AppMessageEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""CriteriaCity"",""CriteriaGender"",""CriteriaAgeMin"",""CriteriaAgeMax"",""CriteriaDeviceOS"",""CriteriaDeviceBrand"",""CriteriaMember"",""Content"",""Weight"",""StartDate"",""EndDate"" FROM ""AppMessage"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var appmessageEntity = DbConnection.Query<AppMessageEntity>(query);
		
			return appmessageEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""AppMessage"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public AppMessageEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""AppMessage"" WHERE ""ID""=@ID";
		
			var appmessageEntity = DbConnection.Query<AppMessageEntity>(query, new {ID=id}).SingleOrDefault();
		
			return appmessageEntity;
		}
		
		public int Update(AppMessageEntity appmessageEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<AppMessageEntity>(appmessageEntity) == false)
			{
				var query = @"UPDATE ""AppMessage"" SET ""CriteriaCity""=@CriteriaCity,""CriteriaGender""=@CriteriaGender,""CriteriaAgeMin""=@CriteriaAgeMin,""CriteriaAgeMax""=@CriteriaAgeMax,""CriteriaDeviceOS""=@CriteriaDeviceOS,""CriteriaDeviceBrand""=@CriteriaDeviceBrand,""CriteriaMember""=@CriteriaMember,""Content""=@Content,""Weight""=@Weight,""StartDate""=@StartDate,""EndDate""=@EndDate WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, appmessageEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int ids)
		{
			var query = @"DELETE FROM  ""AppMessage"" WHERE ""ID"" = @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}