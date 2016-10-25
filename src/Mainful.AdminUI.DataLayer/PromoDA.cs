
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
	public class PromoDA : BaseDA
	{
		public PromoEntity Create(PromoEntity promoEntity)
		{
			var query = @"INSERT INTO ""Promo""(""Title"",""Description"",""StartDate"",""StartTime"",""EndDate"",""EndTime"",""Tag"",""Issuer"",""Days"",""Times"",""Terms"",""Online"",""CreatedDate"",""PromoCategoryID"",""BrandID"",""Priority"",""Slug"") VALUES(@Title,@Description,@StartDate,@StartTime,@EndDate,@EndTime,@Tag,@Issuer,@Days,@Times,@Terms,@Online,@CreatedDate,@PromoCategoryID,@BrandID,@Priority,@Slug) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, promoEntity).Single();
			promoEntity.ID = id;
			return promoEntity;
		}

		public IEnumerable<PromoEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""Title"",""Description"",""StartDate"",""StartTime"",""EndDate"",""EndTime"",""Tag"",""Issuer"",""Days"",""Times"",""Terms"",""Online"",""CreatedDate"",""ModifiedDate"",""PromoCategoryID"",""BrandID"",""Priority"",""Slug"" FROM ""Promo"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var promoEntity = DbConnection.Query<PromoEntity>(query);
		
			return promoEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""Promo"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public PromoEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""Promo"" WHERE ""ID""=@ID";
		
			var promoEntity = DbConnection.Query<PromoEntity>(query, new {ID=id}).SingleOrDefault();
		
			return promoEntity;
		}
		
		public int Update(PromoEntity promoEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<PromoEntity>(promoEntity) == false)
			{
				var query = @"UPDATE ""Promo"" SET ""Title""=@Title,""Description""=@Description,""StartDate""=@StartDate,""StartTime""=@StartTime,""EndDate""=@EndDate,""EndTime""=@EndTime,""Tag""=@Tag,""Issuer""=@Issuer,""Days""=@Days,""Times""=@Times,""Terms""=@Terms,""Online""=@Online,""ModifiedDate""=@ModifiedDate,""PromoCategoryID""=@PromoCategoryID,""BrandID""=@BrandID,""Priority""=@Priority,""Slug""=@Slug WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, promoEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int ids)
		{
			var query = @"DELETE FROM  ""Promo"" WHERE ""ID"" = @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}