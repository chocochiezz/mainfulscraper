
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
	public class AdDA : BaseDA
	{
		public AdEntity Create(AdEntity adEntity)
		{
			var query = @"INSERT INTO ""Ad""(""ContentID"",""ContentSource"",""AdType"",""IsActive"",""StartDate"",""StartTime"",""EndDate"",""EndTime"",""CreatedDate"",""ModifiedDate"",""AdArea"",""Weight"",""StartDateTime"",""EndDateTime"") VALUES(@ContentID,@ContentSource,@AdType,@IsActive,@StartDate,@StartTime,@EndDate,@EndTime,@CreatedDate,@ModifiedDate,@AdArea,@Weight,@StartDateTime,@EndDateTime) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, adEntity).Single();
			adEntity.ID = id;
			return adEntity;
		}

		public IEnumerable<AdEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""ContentID"",""ContentSource"",""AdType"",""IsActive"",""StartDate"",""StartTime"",""EndDate"",""EndTime"",""CreatedDate"",""ModifiedDate"",""AdArea"",""Weight"",""StartDateTime"",""EndDateTime"" FROM ""Ad"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var adEntity = DbConnection.Query<AdEntity>(query);
		
			return adEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""Ad"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public AdEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""Ad"" WHERE ""ID""=@ID";
		
			var adEntity = DbConnection.Query<AdEntity>(query, new {ID=id}).SingleOrDefault();
		
			return adEntity;
		}
		
		public int Update(AdEntity adEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<AdEntity>(adEntity) == false)
			{
				var query = @"UPDATE ""Ad"" SET ""ContentID""=@ContentID,""ContentSource""=@ContentSource,""AdType""=@AdType,""IsActive""=@IsActive,""StartDate""=@StartDate,""StartTime""=@StartTime,""EndDate""=@EndDate,""EndTime""=@EndTime,""CreatedDate""=@CreatedDate,""ModifiedDate""=@ModifiedDate,""AdArea""=@AdArea,""Weight""=@Weight,""StartDateTime""=@StartDateTime,""EndDateTime""=@EndDateTime WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, adEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int ids)
		{
			var query = @"DELETE FROM  ""Ad"" WHERE ""ID"" = @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

        public IEnumerable<ContentEntity> GetContentEvent()
        {
            var query = @"SELECT ""ID"", ""Title"" ""ContentName"" FROM ""Event"" WHERE LOWER(""EventType"")!='seminar' ";

            var contentEntity = DbConnection.Query<ContentEntity>(query);

            return contentEntity;
        }

        public IEnumerable<ContentEntity> GetContentSeminar()
        {
            var query = @"SELECT ""ID"", ""Title"" ""ContentName"" FROM ""Event"" WHERE LOWER(""EventType"")='seminar' ";

            var contentEntity = DbConnection.Query<ContentEntity>(query);

            return contentEntity;
        }

        public IEnumerable<ContentEntity> GetContentPromo()
        {
            var query = @"SELECT ""ID"", ""Title"" ""ContentName"" FROM ""Promo"" ";

            var contentEntity = DbConnection.Query<ContentEntity>(query);

            return contentEntity;
        }
    }
}