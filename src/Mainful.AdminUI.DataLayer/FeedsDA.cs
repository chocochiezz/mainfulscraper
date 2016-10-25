
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
	public class FeedsDA : BaseDA
	{
		public FeedsEntity Create(FeedsEntity feedsEntity)
		{
			var query = @"INSERT INTO ""Feeds""(""FeedChannel"",""Content"",""ImgUrl"",""TargetTags"",""CreatedDate"",""PushDate"",""PriorityWeight"",""PlanPushDate"",""TrackingID"") VALUES(@FeedChannel,@Content,@ImgUrl,@TargetTags,@CreatedDate,@PushDate,@PriorityWeight,@PlanPushDate,@TrackingID) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, feedsEntity).Single();
			feedsEntity.ID = id;
			return feedsEntity;
		}

		public IEnumerable<FeedsEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""FeedChannel"",""Content"",""ImgUrl"",""TargetTags"",""CreatedDate"",""PushDate"",""PriorityWeight"",""PlanPushDate"",""TrackingID"" FROM ""Feeds"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var feedsEntity = DbConnection.Query<FeedsEntity>(query);
		
			return feedsEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""Feeds"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public FeedsEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""Feeds"" WHERE ""ID""=@ID";
		
			var feedsEntity = DbConnection.Query<FeedsEntity>(query, new {ID=id}).SingleOrDefault();
		
			return feedsEntity;
		}
		
		public int Update(FeedsEntity feedsEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<FeedsEntity>(feedsEntity) == false)
			{
				var query = @"UPDATE ""Feeds"" SET ""FeedChannel""=@FeedChannel,""Content""=@Content,""ImgUrl""=@ImgUrl,""TargetTags""=@TargetTags,""PushDate""=@PushDate,""PriorityWeight""=@PriorityWeight,""PlanPushDate""=@PlanPushDate,""TrackingID""=@TrackingID WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, feedsEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int ids)
		{
			var query = @"DELETE FROM  ""Feeds"" WHERE ""ID"" = @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}