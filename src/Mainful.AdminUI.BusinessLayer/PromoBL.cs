using System;
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class PromoBL :BaseBL
    {
        public ResultEntity<PromoEntity> Create(PromoEntity promoEntity)
        {
            var validationResult = new ResultEntity<PromoEntity>();
            promoEntity.CreatedDate = DateTime.Now;

            using (var promoDA = new PromoDA())
			{
				validationResult.Value = promoDA.Create(promoEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<PromoEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<PromoEntity>>();
		
			using (var promoDA = new PromoDA())
			{
				validationResult.Value = promoDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var promoDA = new PromoDA())
			{
				validationResult.Value = promoDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<PromoEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<PromoEntity>();
		
			using (var promoDA = new PromoDA())
			{
				validationResult.Value = promoDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<PromoEntity> Update(PromoEntity promoEntity)
		{
			var validationResult = new ResultEntity<PromoEntity>();
            promoEntity.ModifiedDate = DateTime.Now;
		
			using (var promoDA = new PromoDA())
			{
				var resultUpdate = promoDA.Update(promoEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating Promo!");
					return validationResult;
				}
		
				validationResult.Value = promoEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var promoDA = new PromoDA())
			{
				//var ids = new int[] { id };
				validationResult.Value = promoDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record Promo with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}