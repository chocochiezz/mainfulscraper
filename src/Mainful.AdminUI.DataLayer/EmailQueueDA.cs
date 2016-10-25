
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
	public class EmailQueueDA : BaseDA
	{
		public EmailQueueEntity Create(EmailQueueEntity emailqueueEntity)
		{
			var query = @"INSERT INTO ""EmailQueue""(""Subject"",""FromAddress"",""FromName"",""ToAddress"",""CcAddress"",""BccAddress"",""CreatedDate"",""SendDate"",""ResendDate"",""Body"") VALUES(@Subject,@FromAddress,@FromName,@ToAddress,@CcAddress,@BccAddress,@CreatedDate,@SendDate,@ResendDate,@Body) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, emailqueueEntity).Single();
			emailqueueEntity.ID = id;
			return emailqueueEntity;
		}

		public IEnumerable<EmailQueueEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""Subject"",""FromAddress"",""FromName"",""ToAddress"",""CcAddress"",""BccAddress"",""CreatedDate"",""SendDate"",""ResendDate"",""Body"" FROM ""EmailQueue"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var emailqueueEntity = DbConnection.Query<EmailQueueEntity>(query);
		
			return emailqueueEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""EmailQueue"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public EmailQueueEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""EmailQueue"" WHERE ""ID""=@ID";
		
			var emailqueueEntity = DbConnection.Query<EmailQueueEntity>(query, new {ID=id}).SingleOrDefault();
		
			return emailqueueEntity;
		}
		
		public int Update(EmailQueueEntity emailqueueEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<EmailQueueEntity>(emailqueueEntity) == false)
			{
				var query = @"UPDATE ""EmailQueue"" SET ""Subject""=@Subject,""FromAddress""=@FromAddress,""FromName""=@FromName,""ToAddress""=@ToAddress,""CcAddress""=@CcAddress,""BccAddress""=@BccAddress,""CreatedDate""=@CreatedDate,""SendDate""=@SendDate,""ResendDate""=@ResendDate,""Body""=@Body WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, emailqueueEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int ids)
		{
			var query = @"DELETE FROM  ""EmailQueue"" WHERE ""ID"" = @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}