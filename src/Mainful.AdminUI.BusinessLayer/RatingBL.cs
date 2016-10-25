
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class RatingBL :BaseBL
    {
        public ResultEntity<RatingEntity> Create(RatingEntity ratingEntity)
        {
            var validationResult = new ResultEntity<RatingEntity>();

			using (var ratingDA = new RatingDA())
			{
				validationResult.Value = ratingDA.Create(ratingEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<RatingEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<RatingEntity>>();
		
			using (var ratingDA = new RatingDA())
			{
				validationResult.Value = ratingDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var ratingDA = new RatingDA())
			{
				validationResult.Value = ratingDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<RatingEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<RatingEntity>();
		
			using (var ratingDA = new RatingDA())
			{
				validationResult.Value = ratingDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<RatingEntity> Update(RatingEntity ratingEntity)
		{
			var validationResult = new ResultEntity<RatingEntity>();
		
			using (var ratingDA = new RatingDA())
			{
				var resultUpdate = ratingDA.Update(ratingEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating Rating!");
					return validationResult;
				}
		
				validationResult.Value = ratingEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var ratingDA = new RatingDA())
			{
				//var ids = new int[] { id };
				validationResult.Value = ratingDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record Rating with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}