using System;
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class FeedsBL :BaseBL
    {
        public ResultEntity<FeedsEntity> Create(FeedsEntity feedsEntity)
        {
            var validationResult = new ResultEntity<FeedsEntity>();
            feedsEntity.CreatedDate = DateTime.Now;

			using (var feedsDA = new FeedsDA())
			{
				validationResult.Value = feedsDA.Create(feedsEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<FeedsEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<FeedsEntity>>();
		
			using (var feedsDA = new FeedsDA())
			{
				validationResult.Value = feedsDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var feedsDA = new FeedsDA())
			{
				validationResult.Value = feedsDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<FeedsEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<FeedsEntity>();
		
			using (var feedsDA = new FeedsDA())
			{
				validationResult.Value = feedsDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<FeedsEntity> Update(FeedsEntity feedsEntity)
		{
			var validationResult = new ResultEntity<FeedsEntity>();
		
			using (var feedsDA = new FeedsDA())
			{
				var resultUpdate = feedsDA.Update(feedsEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating Feeds!");
					return validationResult;
				}
		
				validationResult.Value = feedsEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var feedsDA = new FeedsDA())
			{
				//var ids = new int[] { id };
				validationResult.Value = feedsDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record Feeds with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}