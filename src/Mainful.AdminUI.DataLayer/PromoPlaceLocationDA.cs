
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
	public class PromoPlaceLocationDA : BaseDA
	{
		public PromoPlaceLocationEntity Create(PromoPlaceLocationEntity promoplacelocationEntity)
		{
			var query = @"INSERT INTO ""PromoPlaceLocation""(""PlaceID"",""PromoID"") VALUES(@PlaceID,@PromoID) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, promoplacelocationEntity).Single();
			promoplacelocationEntity.ID = id;
			return promoplacelocationEntity;
		}

		public IEnumerable<PromoPlaceLocationEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""PlaceID"",""PromoID"" FROM ""PromoPlaceLocation"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var promoplacelocationEntity = DbConnection.Query<PromoPlaceLocationEntity>(query);
		
			return promoplacelocationEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""PromoPlaceLocation"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public PromoPlaceLocationEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""PromoPlaceLocation"" WHERE ""ID""=@ID";
		
			var promoplacelocationEntity = DbConnection.Query<PromoPlaceLocationEntity>(query, new {ID=id}).SingleOrDefault();
		
			return promoplacelocationEntity;
		}
		
		public int Update(PromoPlaceLocationEntity promoplacelocationEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<PromoPlaceLocationEntity>(promoplacelocationEntity) == false)
			{
				var query = @"UPDATE ""PromoPlaceLocation"" SET ""PlaceID""=@PlaceID,""PromoID""=@PromoID WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, promoplacelocationEntity);
			}
		
			return affectedRows;
		}
		
		public int Delete(int ids)
		{
			var query = @"DELETE FROM  ""PromoPlaceLocation"" WHERE ""ID"" = @Ids";
		
			var affectedRows = DbConnection.Execute(query, new { Ids = ids });
		
			return affectedRows;
		}

	}
}