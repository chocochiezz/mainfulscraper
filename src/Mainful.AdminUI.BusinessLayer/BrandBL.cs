using System;
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class BrandBL :BaseBL
    {
        public ResultEntity<BrandEntity> Create(BrandEntity brandEntity)
        {
            var validationResult = new ResultEntity<BrandEntity>();

			using (var brandDA = new BrandDA())
			{
				validationResult.Value = brandDA.Create(brandEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<BrandEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<BrandEntity>>();
		
			using (var brandDA = new BrandDA())
			{
				validationResult.Value = brandDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var brandDA = new BrandDA())
			{
				validationResult.Value = brandDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<BrandEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<BrandEntity>();
		
			using (var brandDA = new BrandDA())
			{
				validationResult.Value = brandDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<BrandEntity> Update(BrandEntity brandEntity)
		{
			var validationResult = new ResultEntity<BrandEntity>();
            brandEntity.ModifiedDate = DateTime.Now;

            using (var brandDA = new BrandDA())
			{
				var resultUpdate = brandDA.Update(brandEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating Brand!");
					return validationResult;
				}
		
				validationResult.Value = brandEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var brandDA = new BrandDA())
			{
				//var ids = new int[] { id };
				validationResult.Value = brandDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record Brand with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}