
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
	public class SchedulerSessionDA : BaseDA
	{
		public SchedulerSessionEntity Create(SchedulerSessionEntity schedulersessionEntity)
		{
			var query = @"INSERT INTO ""SchedulerSession""(""SessionID"",""Params"",""CreatedDate"") VALUES(@SessionID,@Params,@CreatedDate) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, schedulersessionEntity).Single();
			schedulersessionEntity.ID = id;
			return schedulersessionEntity;
		}

		public IEnumerable<SchedulerSessionEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""SessionID"",""Params"",""ID"",""CreatedDate"" FROM ""SchedulerSession"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var schedulersessionEntity = DbConnection.Query<SchedulerSessionEntity>(query);
		
			return schedulersessionEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""SchedulerSession"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public SchedulerSessionEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""SchedulerSession"" WHERE ""ID""=@ID";
		
			var schedulersessionEntity = DbConnection.Query<SchedulerSessionEntity>(query, new {ID=id}).SingleOrDefault();
		
			return schedulersessionEntity;
		}
		
		public int Update(SchedulerSessionEntity schedulersessionEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<SchedulerSessionEntity>(schedulersessionEntity) == false)
			{
				var query = @"UPDATE ""SchedulerSession"" SET ""SessionID""=@SessionID,""Params""=@Params,""CreatedDate""=@CreatedDate WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, schedulersessionEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int ids)
		{
			var query = @"DELETE FROM  ""SchedulerSession"" WHERE ""ID"" = @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}