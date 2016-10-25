
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
	public class PromoCategoryDA : BaseDA
	{
		public PromoCategoryEntity Create(PromoCategoryEntity promocategoryEntity)
		{
			var query = @"INSERT INTO ""PromoCategory""(""CategoryName"",""Description"",""Logo"",""ModifiedDate"") VALUES(@CategoryName,@Description,@Logo,@ModifiedDate) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, promocategoryEntity).Single();
			promocategoryEntity.ID = id;
			return promocategoryEntity;
		}

		public IEnumerable<PromoCategoryEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""CategoryName"",""Description"",""Logo"",""ModifiedDate"" FROM ""PromoCategory"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var promocategoryEntity = DbConnection.Query<PromoCategoryEntity>(query);
		
			return promocategoryEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""PromoCategory"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public PromoCategoryEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""PromoCategory"" WHERE ""ID""=@ID";
		
			var promocategoryEntity = DbConnection.Query<PromoCategoryEntity>(query, new {ID=id}).SingleOrDefault();
		
			return promocategoryEntity;
		}
		
		public int Update(PromoCategoryEntity promocategoryEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<PromoCategoryEntity>(promocategoryEntity) == false)
			{
				var query = @"UPDATE ""PromoCategory"" SET ""CategoryName""=@CategoryName,""Description""=@Description,""Logo""=@Logo,""ModifiedDate""=@ModifiedDate WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, promocategoryEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int ids)
		{
			var query = @"DELETE FROM  ""PromoCategory"" WHERE ""ID"" = @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}