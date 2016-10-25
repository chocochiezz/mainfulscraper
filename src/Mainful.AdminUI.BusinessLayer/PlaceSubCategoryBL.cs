
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class PlaceSubCategoryBL :BaseBL
    {
        public ResultEntity<PlaceSubCategoryEntity> Create(PlaceSubCategoryEntity placesubcategoryEntity)
        {
            var validationResult = new ResultEntity<PlaceSubCategoryEntity>();

			using (var placesubcategoryDA = new PlaceSubCategoryDA())
			{
				validationResult.Value = placesubcategoryDA.Create(placesubcategoryEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<PlaceSubCategoryEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<PlaceSubCategoryEntity>>();
		
			using (var placesubcategoryDA = new PlaceSubCategoryDA())
			{
				validationResult.Value = placesubcategoryDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var placesubcategoryDA = new PlaceSubCategoryDA())
			{
				validationResult.Value = placesubcategoryDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<PlaceSubCategoryEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<PlaceSubCategoryEntity>();
		
			using (var placesubcategoryDA = new PlaceSubCategoryDA())
			{
				validationResult.Value = placesubcategoryDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<PlaceSubCategoryEntity> Update(PlaceSubCategoryEntity placesubcategoryEntity)
		{
			var validationResult = new ResultEntity<PlaceSubCategoryEntity>();
		
			using (var placesubcategoryDA = new PlaceSubCategoryDA())
			{
				var resultUpdate = placesubcategoryDA.Update(placesubcategoryEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating PlaceSubCategory!");
					return validationResult;
				}
		
				validationResult.Value = placesubcategoryEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var placesubcategoryDA = new PlaceSubCategoryDA())
			{
				//var ids = new int[] { id };
				validationResult.Value = placesubcategoryDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record PlaceSubCategory with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}