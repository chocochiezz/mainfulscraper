
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class UserInterestBL :BaseBL
    {
        public ResultEntity<UserInterestEntity> Create(UserInterestEntity userinterestEntity)
        {
            var validationResult = new ResultEntity<UserInterestEntity>();

			using (var userinterestDA = new UserInterestDA())
			{
				validationResult.Value = userinterestDA.Create(userinterestEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<UserInterestEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<UserInterestEntity>>();
		
			using (var userinterestDA = new UserInterestDA())
			{
				validationResult.Value = userinterestDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var userinterestDA = new UserInterestDA())
			{
				validationResult.Value = userinterestDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<UserInterestEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<UserInterestEntity>();
		
			using (var userinterestDA = new UserInterestDA())
			{
				validationResult.Value = userinterestDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<UserInterestEntity> Update(UserInterestEntity userinterestEntity)
		{
			var validationResult = new ResultEntity<UserInterestEntity>();
		
			using (var userinterestDA = new UserInterestDA())
			{
				var resultUpdate = userinterestDA.Update(userinterestEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating UserInterest!");
					return validationResult;
				}
		
				validationResult.Value = userinterestEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var userinterestDA = new UserInterestDA())
			{
				//var ids = new int[] { id };
				validationResult.Value = userinterestDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record UserInterest with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}