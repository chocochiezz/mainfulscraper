
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
	public class BrandDA : BaseDA
	{
		public BrandEntity Create(BrandEntity brandEntity)
		{
			var query = @"INSERT INTO ""Brand""(""BrandName"",""Description"",""Weblink"",""Facebook"",""Twitter"",""GooglePlus"",""Email"",""Phone"",""Instagram"",""Logo"",""LogoChecksum"") VALUES(@BrandName,@Description,@Weblink,@Facebook,@Twitter,@GooglePlus,@Email,@Phone,@Instagram,@Logo,@LogoChecksum) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, brandEntity).Single();
			brandEntity.ID = id;
			return brandEntity;
		}

		public IEnumerable<BrandEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""BrandName"",""Description"",""Weblink"",""Facebook"",""Twitter"",""GooglePlus"",""Email"",""Phone"",""Instagram"",""Logo"",""ModifiedDate"",""LogoChecksum"" FROM ""Brand"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var brandEntity = DbConnection.Query<BrandEntity>(query);
		
			return brandEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""Brand"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public BrandEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""Brand"" WHERE ""ID""=@ID";
		
			var brandEntity = DbConnection.Query<BrandEntity>(query, new {ID=id}).SingleOrDefault();
		
			return brandEntity;
		}
		
		public int Update(BrandEntity brandEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<BrandEntity>(brandEntity) == false)
			{
				var query = @"UPDATE ""Brand"" SET ""BrandName""=@BrandName,""Description""=@Description,""Weblink""=@Weblink,""Facebook""=@Facebook,""Twitter""=@Twitter,""GooglePlus""=@GooglePlus,""Email""=@Email,""Phone""=@Phone,""Instagram""=@Instagram,""Logo""=@Logo,""LogoChecksum""=@LogoChecksum,""ModifiedDate""=@ModifiedDate WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, brandEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int ids)
		{
			var query = @"DELETE FROM  ""Brand"" WHERE ""ID"" = @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}