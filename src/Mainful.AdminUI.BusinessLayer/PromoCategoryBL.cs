
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class PromoCategoryBL :BaseBL
    {
        public ResultEntity<PromoCategoryEntity> Create(PromoCategoryEntity promocategoryEntity)
        {
            var validationResult = new ResultEntity<PromoCategoryEntity>();

			using (var promocategoryDA = new PromoCategoryDA())
			{
				validationResult.Value = promocategoryDA.Create(promocategoryEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<PromoCategoryEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<PromoCategoryEntity>>();
		
			using (var promocategoryDA = new PromoCategoryDA())
			{
				validationResult.Value = promocategoryDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var promocategoryDA = new PromoCategoryDA())
			{
				validationResult.Value = promocategoryDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<PromoCategoryEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<PromoCategoryEntity>();
		
			using (var promocategoryDA = new PromoCategoryDA())
			{
				validationResult.Value = promocategoryDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<PromoCategoryEntity> Update(PromoCategoryEntity promocategoryEntity)
		{
			var validationResult = new ResultEntity<PromoCategoryEntity>();
		
			using (var promocategoryDA = new PromoCategoryDA())
			{
				var resultUpdate = promocategoryDA.Update(promocategoryEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating PromoCategory!");
					return validationResult;
				}
		
				validationResult.Value = promocategoryEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var promocategoryDA = new PromoCategoryDA())
			{
				//var ids = new int[] { id };
				validationResult.Value = promocategoryDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record PromoCategory with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}