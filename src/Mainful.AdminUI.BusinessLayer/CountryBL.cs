
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class CountryBL :BaseBL
    {
        public ResultEntity<CountryEntity> Create(CountryEntity countryEntity)
        {
            var validationResult = new ResultEntity<CountryEntity>();

			using (var countryDA = new CountryDA())
			{
				validationResult.Value = countryDA.Create(countryEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<CountryEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<CountryEntity>>();
		
			using (var countryDA = new CountryDA())
			{
				validationResult.Value = countryDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var countryDA = new CountryDA())
			{
				validationResult.Value = countryDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<CountryEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<CountryEntity>();
		
			using (var countryDA = new CountryDA())
			{
				validationResult.Value = countryDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<CountryEntity> Update(CountryEntity countryEntity)
		{
			var validationResult = new ResultEntity<CountryEntity>();
		
			using (var countryDA = new CountryDA())
			{
				var resultUpdate = countryDA.Update(countryEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating Country!");
					return validationResult;
				}
		
				validationResult.Value = countryEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var countryDA = new CountryDA())
			{
				var ids = new int[] { id };
				validationResult.Value = countryDA.Delete(ids);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record Country with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}