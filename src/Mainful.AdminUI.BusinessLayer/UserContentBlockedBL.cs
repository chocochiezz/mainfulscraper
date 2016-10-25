
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class UserContentBlockedBL :BaseBL
    {
        public ResultEntity<UserContentBlockedEntity> Create(UserContentBlockedEntity usercontentblockedEntity)
        {
            var validationResult = new ResultEntity<UserContentBlockedEntity>();

			using (var usercontentblockedDA = new UserContentBlockedDA())
			{
				validationResult.Value = usercontentblockedDA.Create(usercontentblockedEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<UserContentBlockedEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<UserContentBlockedEntity>>();
		
			using (var usercontentblockedDA = new UserContentBlockedDA())
			{
				validationResult.Value = usercontentblockedDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var usercontentblockedDA = new UserContentBlockedDA())
			{
				validationResult.Value = usercontentblockedDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<UserContentBlockedEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<UserContentBlockedEntity>();
		
			using (var usercontentblockedDA = new UserContentBlockedDA())
			{
				validationResult.Value = usercontentblockedDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<UserContentBlockedEntity> Update(UserContentBlockedEntity usercontentblockedEntity)
		{
			var validationResult = new ResultEntity<UserContentBlockedEntity>();
		
			using (var usercontentblockedDA = new UserContentBlockedDA())
			{
				var resultUpdate = usercontentblockedDA.Update(usercontentblockedEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating UserContentBlocked!");
					return validationResult;
				}
		
				validationResult.Value = usercontentblockedEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var usercontentblockedDA = new UserContentBlockedDA())
			{
				//var ids = new int[] { id };
				validationResult.Value = usercontentblockedDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record UserContentBlocked with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}