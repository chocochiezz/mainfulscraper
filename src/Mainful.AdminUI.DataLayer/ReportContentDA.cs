
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
	public class ReportContentDA : BaseDA
	{
		public ReportContentEntity Create(ReportContentEntity reportcontentEntity)
		{
			var query = @"INSERT INTO ""ReportContent""(""Content"",""Category"",""Rating"",""Context"",""ReffID"",""Approved"",""Comment"",""CreatedDate"") VALUES(@Content,@Category,@Rating,@Context,@ReffID,@Approved,@Comment,@CreatedDate) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, reportcontentEntity).Single();
			reportcontentEntity.ID = id;
			return reportcontentEntity;
		}

		public IEnumerable<ReportContentEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""Content"",""Category"",""Rating"",""Context"",""ReffID"",""Approved"",""Comment"",""CreatedDate"" FROM ""ReportContent"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var reportcontentEntity = DbConnection.Query<ReportContentEntity>(query);
		
			return reportcontentEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""ReportContent"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public ReportContentEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""ReportContent"" WHERE ""ID""=@ID";
		
			var reportcontentEntity = DbConnection.Query<ReportContentEntity>(query, new {ID=id}).SingleOrDefault();
		
			return reportcontentEntity;
		}
		
		public int Update(ReportContentEntity reportcontentEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<ReportContentEntity>(reportcontentEntity) == false)
			{
				var query = @"UPDATE ""ReportContent"" SET ""Content""=@Content,""Category""=@Category,""Rating""=@Rating,""Context""=@Context,""ReffID""=@ReffID,""Approved""=@Approved,""Comment""=@Comment,""CreatedDate""=@CreatedDate WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, reportcontentEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int ids)
		{
			var query = @"DELETE FROM  ""ReportContent"" WHERE ""ID"" = @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}