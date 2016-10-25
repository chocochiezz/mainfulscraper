
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class PlaceCategoryBL :BaseBL
    {
        public ResultEntity<PlaceCategoryEntity> Create(PlaceCategoryEntity placecategoryEntity)
        {
            var validationResult = new ResultEntity<PlaceCategoryEntity>();

			using (var placecategoryDA = new PlaceCategoryDA())
			{
				validationResult.Value = placecategoryDA.Create(placecategoryEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<PlaceCategoryEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<PlaceCategoryEntity>>();
		
			using (var placecategoryDA = new PlaceCategoryDA())
			{
				validationResult.Value = placecategoryDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var placecategoryDA = new PlaceCategoryDA())
			{
				validationResult.Value = placecategoryDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<PlaceCategoryEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<PlaceCategoryEntity>();
		
			using (var placecategoryDA = new PlaceCategoryDA())
			{
				validationResult.Value = placecategoryDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<PlaceCategoryEntity> Update(PlaceCategoryEntity placecategoryEntity)
		{
			var validationResult = new ResultEntity<PlaceCategoryEntity>();
		
			using (var placecategoryDA = new PlaceCategoryDA())
			{
				var resultUpdate = placecategoryDA.Update(placecategoryEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating PlaceCategory!");
					return validationResult;
				}
		
				validationResult.Value = placecategoryEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var placecategoryDA = new PlaceCategoryDA())
			{
				//var ids = new int[] { id };
				validationResult.Value = placecategoryDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record PlaceCategory with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}