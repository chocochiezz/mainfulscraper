
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
	public class EventImageDA : BaseDA
	{
		public EventImageEntity Create(EventImageEntity eventimageEntity)
		{
			var query = @"INSERT INTO ""EventImage""(""IsMain"",""EventID"",""Content"",""ModifiedDate"",""Checksum"") VALUES(@IsMain,@EventID,@Content,@ModifiedDate,@Checksum) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, eventimageEntity).Single();
			eventimageEntity.ID = id;
			return eventimageEntity;
		}

		public IEnumerable<EventImageEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""IsMain"",""EventID"",""Content"",""ModifiedDate"",""Checksum"" FROM ""EventImage"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var eventimageEntity = DbConnection.Query<EventImageEntity>(query);
		
			return eventimageEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""EventImage"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public EventImageEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""EventImage"" WHERE ""ID""=@ID";
		
			var eventimageEntity = DbConnection.Query<EventImageEntity>(query, new {ID=id}).SingleOrDefault();
		
			return eventimageEntity;
		}
		
		public int Update(EventImageEntity eventimageEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<EventImageEntity>(eventimageEntity) == false)
			{
				var query = @"UPDATE ""EventImage"" SET ""IsMain""=@IsMain,""EventID""=@EventID,""Content""=@Content,""ModifiedDate""=@ModifiedDate,""Checksum""=@Checksum WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, eventimageEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int ids)
		{
			var query = @"DELETE FROM  ""EventImage"" WHERE ""ID"" = @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}