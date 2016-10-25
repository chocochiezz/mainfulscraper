
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
	public class TrackingDA : BaseDA
	{
		public TrackingEntity Create(TrackingEntity trackingEntity)
		{
			var query = @"INSERT INTO ""Tracking""(""TrackingID"",""CreatedDate"",""UserID"",""DeviceModel"",""DeviceBrand"",""DeviceID"",""UserFullname"",""UserEmail"",""Channel"",""ClientID"",""TrackingChannel"",""Params"") VALUES(@TrackingID,@CreatedDate,@UserID,@DeviceModel,@DeviceBrand,@DeviceID,@UserFullname,@UserEmail,@Channel,@ClientID,@TrackingChannel,@Params) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, trackingEntity).Single();
			trackingEntity.ID = id;
			return trackingEntity;
		}

		public IEnumerable<TrackingEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""TrackingID"",""CreatedDate"",""UserID"",""DeviceModel"",""DeviceBrand"",""DeviceID"",""UserFullname"",""UserEmail"",""Channel"",""ClientID"",""TrackingChannel"",""Params"" FROM ""Tracking"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var trackingEntity = DbConnection.Query<TrackingEntity>(query);
		
			return trackingEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""Tracking"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public TrackingEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""Tracking"" WHERE ""ID""=@ID";
		
			var trackingEntity = DbConnection.Query<TrackingEntity>(query, new {ID=id}).SingleOrDefault();
		
			return trackingEntity;
		}
		
		public int Update(TrackingEntity trackingEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<TrackingEntity>(trackingEntity) == false)
			{
				var query = @"UPDATE ""Tracking"" SET ""TrackingID""=@TrackingID,""CreatedDate""=@CreatedDate,""UserID""=@UserID,""DeviceModel""=@DeviceModel,""DeviceBrand""=@DeviceBrand,""DeviceID""=@DeviceID,""UserFullname""=@UserFullname,""UserEmail""=@UserEmail,""Channel""=@Channel,""ClientID""=@ClientID,""TrackingChannel""=@TrackingChannel,""Params""=@Params WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, trackingEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int ids)
		{
			var query = @"DELETE FROM  ""Tracking"" WHERE ""ID"" = @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}