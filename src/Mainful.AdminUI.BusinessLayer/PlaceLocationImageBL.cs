
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class PlaceLocationImageBL :BaseBL
    {
        public ResultEntity<PlaceLocationImageEntity> Create(PlaceLocationImageEntity placelocationimageEntity)
        {
            var validationResult = new ResultEntity<PlaceLocationImageEntity>();

			using (var placelocationimageDA = new PlaceLocationImageDA())
			{
				validationResult.Value = placelocationimageDA.Create(placelocationimageEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<PlaceLocationImageEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<PlaceLocationImageEntity>>();
		
			using (var placelocationimageDA = new PlaceLocationImageDA())
			{
				validationResult.Value = placelocationimageDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var placelocationimageDA = new PlaceLocationImageDA())
			{
				validationResult.Value = placelocationimageDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<PlaceLocationImageEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<PlaceLocationImageEntity>();
		
			using (var placelocationimageDA = new PlaceLocationImageDA())
			{
				validationResult.Value = placelocationimageDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<PlaceLocationImageEntity> Update(PlaceLocationImageEntity placelocationimageEntity)
		{
			var validationResult = new ResultEntity<PlaceLocationImageEntity>();
		
			using (var placelocationimageDA = new PlaceLocationImageDA())
			{
				var resultUpdate = placelocationimageDA.Update(placelocationimageEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating PlaceLocationImage!");
					return validationResult;
				}
		
				validationResult.Value = placelocationimageEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var placelocationimageDA = new PlaceLocationImageDA())
			{
				//var ids = new int[] { id };
				validationResult.Value = placelocationimageDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record PlaceLocationImage with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}