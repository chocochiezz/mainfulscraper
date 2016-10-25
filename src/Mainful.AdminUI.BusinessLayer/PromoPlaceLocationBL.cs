
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class PromoPlaceLocationBL :BaseBL
    {
        public ResultEntity<PromoPlaceLocationEntity> Create(PromoPlaceLocationEntity promoplacelocationEntity)
        {
            var validationResult = new ResultEntity<PromoPlaceLocationEntity>();

			using (var promoplacelocationDA = new PromoPlaceLocationDA())
			{
				validationResult.Value = promoplacelocationDA.Create(promoplacelocationEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<PromoPlaceLocationEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<PromoPlaceLocationEntity>>();
		
			using (var promoplacelocationDA = new PromoPlaceLocationDA())
			{
				validationResult.Value = promoplacelocationDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var promoplacelocationDA = new PromoPlaceLocationDA())
			{
				validationResult.Value = promoplacelocationDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<PromoPlaceLocationEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<PromoPlaceLocationEntity>();
		
			using (var promoplacelocationDA = new PromoPlaceLocationDA())
			{
				validationResult.Value = promoplacelocationDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<PromoPlaceLocationEntity> Update(PromoPlaceLocationEntity promoplacelocationEntity)
		{
			var validationResult = new ResultEntity<PromoPlaceLocationEntity>();
		
			using (var promoplacelocationDA = new PromoPlaceLocationDA())
			{
				var resultUpdate = promoplacelocationDA.Update(promoplacelocationEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating PromoPlaceLocation!");
					return validationResult;
				}
		
				validationResult.Value = promoplacelocationEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var promoplacelocationDA = new PromoPlaceLocationDA())
			{
				//var ids = new int[] { id };
				validationResult.Value = promoplacelocationDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record PromoPlaceLocation with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}