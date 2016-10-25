
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class CityBL :BaseBL
    {
        public ResultEntity<CityEntity> Create(CityEntity cityEntity)
        {
            var validationResult = new ResultEntity<CityEntity>();

			using (var cityDA = new CityDA())
			{
				validationResult.Value = cityDA.Create(cityEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<CityEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<CityEntity>>();
		
			using (var cityDA = new CityDA())
			{
				validationResult.Value = cityDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var cityDA = new CityDA())
			{
				validationResult.Value = cityDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<CityEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<CityEntity>();
		
			using (var cityDA = new CityDA())
			{
				validationResult.Value = cityDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<CityEntity> Update(CityEntity cityEntity)
		{
			var validationResult = new ResultEntity<CityEntity>();
		
			using (var cityDA = new CityDA())
			{
				var resultUpdate = cityDA.Update(cityEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating City!");
					return validationResult;
				}
		
				validationResult.Value = cityEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var cityDA = new CityDA())
			{
				var ids = new int[] { id };
				validationResult.Value = cityDA.Delete(ids);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record City with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}