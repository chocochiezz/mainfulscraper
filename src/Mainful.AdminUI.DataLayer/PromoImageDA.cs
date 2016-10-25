
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
	public class PromoImageDA : BaseDA
	{
		public PromoImageEntity Create(PromoImageEntity promoimageEntity)
		{
			var query = @"INSERT INTO ""PromoImage""(""IsMain"",""PromoID"",""Content"",""ModifiedDate"",""Checksum"") VALUES(@IsMain,@PromoID,@Content,@ModifiedDate,@Checksum) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, promoimageEntity).Single();
			promoimageEntity.ID = id;
			return promoimageEntity;
		}

		public IEnumerable<PromoImageEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""IsMain"",""PromoID"",""Content"",""ModifiedDate"",""Checksum"" FROM ""PromoImage"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var promoimageEntity = DbConnection.Query<PromoImageEntity>(query);
		
			return promoimageEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""PromoImage"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public PromoImageEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""PromoImage"" WHERE ""ID""=@ID";
		
			var promoimageEntity = DbConnection.Query<PromoImageEntity>(query, new {ID=id}).SingleOrDefault();
		
			return promoimageEntity;
		}
		
		public int Update(PromoImageEntity promoimageEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<PromoImageEntity>(promoimageEntity) == false)
			{
				var query = @"UPDATE ""PromoImage"" SET ""IsMain""=@IsMain,""PromoID""=@PromoID,""Content""=@Content,""ModifiedDate""=@ModifiedDate,""Checksum""=@Checksum WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, promoimageEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int ids)
		{
			var query = @"DELETE FROM  ""PromoImage"" WHERE ""ID"" = @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}