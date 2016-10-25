
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
	public class ClientDA : BaseDA
	{
		public ClientEntity Create(ClientEntity clientEntity)
		{
			var query = @"INSERT INTO ""Client""(""Channel"",""ClientID"",""SecretKey"",""CreatedDate"",""ModifiedDate"") VALUES(@Channel,@ClientID,@SecretKey,@CreatedDate,@ModifiedDate) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, clientEntity).Single();
			clientEntity.ID = id;
			return clientEntity;
		}

		public IEnumerable<ClientEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""Channel"",""ClientID"",""SecretKey"",""CreatedDate"",""ModifiedDate"" FROM ""Client"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var clientEntity = DbConnection.Query<ClientEntity>(query);
		
			return clientEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""Client"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public ClientEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""Client"" WHERE ""ID""=@ID";
		
			var clientEntity = DbConnection.Query<ClientEntity>(query, new {ID=id}).SingleOrDefault();
		
			return clientEntity;
		}
		
		public int Update(ClientEntity clientEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<ClientEntity>(clientEntity) == false)
			{
				var query = @"UPDATE ""Client"" SET ""Channel""=@Channel,""ClientID""=@ClientID,""SecretKey""=@SecretKey,""CreatedDate""=@CreatedDate,""ModifiedDate""=@ModifiedDate WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, clientEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int ids)
		{
			var query = @"DELETE FROM  ""Client"" WHERE ""ID"" = @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}