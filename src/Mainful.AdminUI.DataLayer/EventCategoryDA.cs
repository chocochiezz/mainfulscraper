
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
	public class EventCategoryDA : BaseDA
	{
		public EventCategoryEntity Create(EventCategoryEntity eventcategoryEntity)
		{
			var query = @"INSERT INTO ""EventCategory""(""CategoryName"",""Description"",""Logo"",""ModifiedDate"") VALUES(@CategoryName,@Description,@Logo,@ModifiedDate) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, eventcategoryEntity).Single();
			eventcategoryEntity.ID = id;
			return eventcategoryEntity;
		}

		public IEnumerable<EventCategoryEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""CategoryName"",""Description"",""Logo"",""ModifiedDate"" FROM ""EventCategory"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var eventcategoryEntity = DbConnection.Query<EventCategoryEntity>(query);
		
			return eventcategoryEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""EventCategory"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public EventCategoryEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""EventCategory"" WHERE ""ID""=@ID";
		
			var eventcategoryEntity = DbConnection.Query<EventCategoryEntity>(query, new {ID=id}).SingleOrDefault();
		
			return eventcategoryEntity;
		}
		
		public int Update(EventCategoryEntity eventcategoryEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<EventCategoryEntity>(eventcategoryEntity) == false)
			{
				var query = @"UPDATE ""EventCategory"" SET ""CategoryName""=@CategoryName,""Description""=@Description,""Logo""=@Logo,""ModifiedDate""=@ModifiedDate WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, eventcategoryEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int ids)
		{
			var query = @"DELETE FROM  ""EventCategory"" WHERE ""ID"" = @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}