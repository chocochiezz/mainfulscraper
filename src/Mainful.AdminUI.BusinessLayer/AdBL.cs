
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class AdBL :BaseBL
    {
        public ResultEntity<AdEntity> Create(AdEntity adEntity)
        {
            var validationResult = new ResultEntity<AdEntity>();

			using (var adDA = new AdDA())
			{
				validationResult.Value = adDA.Create(adEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<AdEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<AdEntity>>();
		
			using (var adDA = new AdDA())
			{
				validationResult.Value = adDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var adDA = new AdDA())
			{
				validationResult.Value = adDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<AdEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<AdEntity>();
		
			using (var adDA = new AdDA())
			{
				validationResult.Value = adDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<AdEntity> Update(AdEntity adEntity)
		{
			var validationResult = new ResultEntity<AdEntity>();
		
			using (var adDA = new AdDA())
			{
				var resultUpdate = adDA.Update(adEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating Ad!");
					return validationResult;
				}
		
				validationResult.Value = adEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var adDA = new AdDA())
			{
				//var ids = new int[] { id };
				validationResult.Value = adDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record Ad with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}

        public ResultEntity<IEnumerable<ContentEntity>> GetContentEvent()
        {
            var validationResult = new ResultEntity<IEnumerable<ContentEntity>>();

            using (var adDA = new AdDA())
            {
                validationResult.Value = adDA.GetContentEvent();
            }

            return validationResult;
        }

        public ResultEntity<IEnumerable<ContentEntity>> GetContentSeminar()
        {
            var validationResult = new ResultEntity<IEnumerable<ContentEntity>>();

            using (var adDA = new AdDA())
            {
                validationResult.Value = adDA.GetContentSeminar();
            }

            return validationResult;
        }

        public ResultEntity<IEnumerable<ContentEntity>> GetContentPromo()
        {
            var validationResult = new ResultEntity<IEnumerable<ContentEntity>>();

            using (var adDA = new AdDA())
            {
                validationResult.Value = adDA.GetContentPromo();
            }

            return validationResult;
        }
    }
}