
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
	public class EventPlaceLocationDA : BaseDA
	{
		public EventPlaceLocationEntity Create(EventPlaceLocationEntity eventplacelocationEntity)
		{
			var query = @"INSERT INTO ""EventPlaceLocation""(""PlaceID"",""EventID"") VALUES(@PlaceID,@EventID) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, eventplacelocationEntity).Single();
			eventplacelocationEntity.ID = id;
			return eventplacelocationEntity;
		}

		public IEnumerable<EventPlaceLocationEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""PlaceID"",""EventID"" FROM ""EventPlaceLocation"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var eventplacelocationEntity = DbConnection.Query<EventPlaceLocationEntity>(query);
		
			return eventplacelocationEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""EventPlaceLocation"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public EventPlaceLocationEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""EventPlaceLocation"" WHERE ""ID""=@ID";
		
			var eventplacelocationEntity = DbConnection.Query<EventPlaceLocationEntity>(query, new {ID=id}).SingleOrDefault();
		
			return eventplacelocationEntity;
		}
		
		public int Update(EventPlaceLocationEntity eventplacelocationEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<EventPlaceLocationEntity>(eventplacelocationEntity) == false)
			{
				var query = @"UPDATE ""EventPlaceLocation"" SET ""PlaceID""=@PlaceID,""EventID""=@EventID WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, eventplacelocationEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int ids)
		{
			var query = @"DELETE FROM  ""EventPlaceLocation"" WHERE ""ID"" = @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}