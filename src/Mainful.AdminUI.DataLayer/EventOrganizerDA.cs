
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
	public class EventOrganizerDA : BaseDA
	{
		public EventOrganizerEntity Create(EventOrganizerEntity eventorganizerEntity)
		{
			var query = @"INSERT INTO ""EventOrganizer""(""Name"",""Description"",""Phone1"",""Phone2"",""Logo"",""ShortName"",""LongDescription"",""LogoChecksum"") VALUES(@Name,@Description,@Phone1,@Phone2,@Logo,@ShortName,@LongDescription,@LogoChecksum) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, eventorganizerEntity).Single();
			eventorganizerEntity.ID = id;
			return eventorganizerEntity;
		}

		public IEnumerable<EventOrganizerEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""Name"",""Description"",""Phone1"",""Phone2"",""Logo"",""ShortName"",""LongDescription"",""ModifiedDate"",""LogoChecksum"" FROM ""EventOrganizer"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var eventorganizerEntity = DbConnection.Query<EventOrganizerEntity>(query);
		
			return eventorganizerEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""EventOrganizer"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public EventOrganizerEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""EventOrganizer"" WHERE ""ID""=@ID";
		
			var eventorganizerEntity = DbConnection.Query<EventOrganizerEntity>(query, new {ID=id}).SingleOrDefault();
		
			return eventorganizerEntity;
		}
		
		public int Update(EventOrganizerEntity eventorganizerEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<EventOrganizerEntity>(eventorganizerEntity) == false)
			{
				var query = @"UPDATE ""EventOrganizer"" SET ""Name""=@Name,""Description""=@Description,""Phone1""=@Phone1,""Phone2""=@Phone2,""Logo""=@Logo,""ShortName""=@ShortName,""LongDescription""=@LongDescription,""ModifiedDate""=@ModifiedDate,""LogoChecksum""=@LogoChecksum WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, eventorganizerEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int ids)
		{
			var query = @"DELETE FROM  ""EventOrganizer"" WHERE ""ID"" = @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}