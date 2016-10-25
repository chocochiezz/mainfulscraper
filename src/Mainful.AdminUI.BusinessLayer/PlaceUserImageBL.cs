
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class PlaceUserImageBL :BaseBL
    {
        public ResultEntity<PlaceUserImageEntity> Create(PlaceUserImageEntity placeuserimageEntity)
        {
            var validationResult = new ResultEntity<PlaceUserImageEntity>();

			using (var placeuserimageDA = new PlaceUserImageDA())
			{
				validationResult.Value = placeuserimageDA.Create(placeuserimageEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<PlaceUserImageEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<PlaceUserImageEntity>>();
		
			using (var placeuserimageDA = new PlaceUserImageDA())
			{
				validationResult.Value = placeuserimageDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var placeuserimageDA = new PlaceUserImageDA())
			{
				validationResult.Value = placeuserimageDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<PlaceUserImageEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<PlaceUserImageEntity>();
		
			using (var placeuserimageDA = new PlaceUserImageDA())
			{
				validationResult.Value = placeuserimageDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<PlaceUserImageEntity> Update(PlaceUserImageEntity placeuserimageEntity)
		{
			var validationResult = new ResultEntity<PlaceUserImageEntity>();
		
			using (var placeuserimageDA = new PlaceUserImageDA())
			{
				var resultUpdate = placeuserimageDA.Update(placeuserimageEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating PlaceUserImage!");
					return validationResult;
				}
		
				validationResult.Value = placeuserimageEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var placeuserimageDA = new PlaceUserImageDA())
			{
				//var ids = new int[] { id };
				validationResult.Value = placeuserimageDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record PlaceUserImage with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}