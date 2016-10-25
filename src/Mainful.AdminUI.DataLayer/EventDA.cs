
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
	public class EventDA : BaseDA
	{
		public EventEntity Create(EventEntity eventEntity)
		{
			var query = @"INSERT INTO ""Event""(""Title"",""Description"",""StartDate"",""StartTime"",""EndDate"",""EndTime"",""Timezone"",""Weblink"",""Facebook"",""Twitter"",""GooglePlus"",""Email"",""Online"",""CreatedDate"",""EventCategoryID"",""EventOrganizerID"",""Tag"",""IsFree"",""Currency"",""PriceRange"",""Priority"",""EventType"",""Slug"") VALUES(@Title,@Description,@StartDate,@StartTime,@EndDate,@EndTime,@Timezone,@Weblink,@Facebook,@Twitter,@GooglePlus,@Email,@Online,@CreatedDate,@EventCategoryID,@EventOrganizerID,@Tag,@IsFree,@Currency,@PriceRange,@Priority,@EventType,@Slug) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, eventEntity).Single();
			eventEntity.ID = id;
			return eventEntity;
		}

		public IEnumerable<EventEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""Title"",""Description"",""StartDate"",""StartTime"",""EndDate"",""EndTime"",""Timezone"",""Weblink"",""Facebook"",""Twitter"",""GooglePlus"",""Email"",""Online"",""CreatedDate"",""ModifiedDate"",""EventCategoryID"",""EventOrganizerID"",""Tag"",""IsFree"",""Currency"",""PriceRange"",""Priority"",""EventType"",""Slug"" FROM ""Event"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var eventEntity = DbConnection.Query<EventEntity>(query);
		
			return eventEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""Event"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public EventEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""Event"" WHERE ""ID""=@ID";
		
			var eventEntity = DbConnection.Query<EventEntity>(query, new {ID=id}).SingleOrDefault();
		
			return eventEntity;
		}
		
		public int Update(EventEntity eventEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<EventEntity>(eventEntity) == false)
			{
				var query = @"UPDATE ""Event"" SET ""Title""=@Title,""Description""=@Description,""StartDate""=@StartDate,""StartTime""=@StartTime,""EndDate""=@EndDate,""EndTime""=@EndTime,""Timezone""=@Timezone,""Weblink""=@Weblink,""Facebook""=@Facebook,""Twitter""=@Twitter,""GooglePlus""=@GooglePlus,""Email""=@Email,""Online""=@Online,""ModifiedDate""=@ModifiedDate,""EventCategoryID""=@EventCategoryID,""EventOrganizerID""=@EventOrganizerID,""Tag""=@Tag,""IsFree""=@IsFree,""Currency""=@Currency,""PriceRange""=@PriceRange,""Priority""=@Priority,""EventType""=@EventType,""Slug""=@Slug WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, eventEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int ids)
		{
			var query = @"DELETE FROM  ""Event"" WHERE ""ID"" = @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}