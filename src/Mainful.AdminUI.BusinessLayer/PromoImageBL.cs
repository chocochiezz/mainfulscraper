
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class PromoImageBL :BaseBL
    {
        public ResultEntity<PromoImageEntity> Create(PromoImageEntity promoimageEntity)
        {
            var validationResult = new ResultEntity<PromoImageEntity>();

			using (var promoimageDA = new PromoImageDA())
			{
				validationResult.Value = promoimageDA.Create(promoimageEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<PromoImageEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<PromoImageEntity>>();
		
			using (var promoimageDA = new PromoImageDA())
			{
				validationResult.Value = promoimageDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var promoimageDA = new PromoImageDA())
			{
				validationResult.Value = promoimageDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<PromoImageEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<PromoImageEntity>();
		
			using (var promoimageDA = new PromoImageDA())
			{
				validationResult.Value = promoimageDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<PromoImageEntity> Update(PromoImageEntity promoimageEntity)
		{
			var validationResult = new ResultEntity<PromoImageEntity>();
		
			using (var promoimageDA = new PromoImageDA())
			{
				var resultUpdate = promoimageDA.Update(promoimageEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating PromoImage!");
					return validationResult;
				}
		
				validationResult.Value = promoimageEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var promoimageDA = new PromoImageDA())
			{
				//var ids = new int[] { id };
				validationResult.Value = promoimageDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record PromoImage with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}