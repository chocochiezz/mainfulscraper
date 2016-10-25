
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
	public class GroupAdministratorDA : BaseDA
	{
		public GroupAdministratorEntity Create(GroupAdministratorEntity groupadministratorEntity)
		{
			var query = @"INSERT INTO ""GroupAdministrator""(""GroupName"",""Description"",""CreatedBy"",""CreatedDate"") VALUES(@GroupName,@Description,@CreatedBy,@CreatedDate) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, groupadministratorEntity).Single();
			groupadministratorEntity.ID = id;
			return groupadministratorEntity;
		}

		public IEnumerable<GroupAdministratorEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""GroupName"",""Description"",""CreatedBy"",""ModifiedBy"",""CreatedDate"",""ModifiedDate"",""IsDeleted"" FROM ""GroupAdministrator"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var groupadministratorEntity = DbConnection.Query<GroupAdministratorEntity>(query);
		
			return groupadministratorEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""GroupAdministrator"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public GroupAdministratorEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""GroupAdministrator"" WHERE ""ID""=@ID AND ""IsDeleted""=false";
		
			var groupadministratorEntity = DbConnection.Query<GroupAdministratorEntity>(query, new {ID=id}).SingleOrDefault();
		
			return groupadministratorEntity;
		}

        public IEnumerable<GroupAdministratorEntity> GetByGroupName(string groupName)
        {
            var query = @"SELECT * FROM ""GroupAdministrator"" WHERE ""GroupName""=@GroupName AND ""IsDeleted""=false";
            var groupadministratorEntity = DbConnection.Query<GroupAdministratorEntity>(query, new { GroupName = groupName });

            return groupadministratorEntity;
        }

        public int Update(GroupAdministratorEntity groupadministratorEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<GroupAdministratorEntity>(groupadministratorEntity) == false)
			{
				var query = @"UPDATE ""GroupAdministrator"" SET ""GroupName""=@GroupName,""Description""=@Description,""ModifiedBy""=@ModifiedBy,""ModifiedDate""=@ModifiedDate WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, groupadministratorEntity);
			}
		
			return affectedRows;
		}

        public int Delete(int ids)
        {
            var query = @"UPDATE ""GroupAdministrator"" SET ""IsDeleted""=true WHERE ""ID""=@ID";
            var affectedRows = DbConnection.Execute(query, new { ID = ids });

            return affectedRows;
        }
	}
}