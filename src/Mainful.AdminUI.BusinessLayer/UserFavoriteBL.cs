
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class UserFavoriteBL :BaseBL
    {
        public ResultEntity<UserFavoriteEntity> Create(UserFavoriteEntity userfavoriteEntity)
        {
            var validationResult = new ResultEntity<UserFavoriteEntity>();

			using (var userfavoriteDA = new UserFavoriteDA())
			{
				validationResult.Value = userfavoriteDA.Create(userfavoriteEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<UserFavoriteEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<UserFavoriteEntity>>();
		
			using (var userfavoriteDA = new UserFavoriteDA())
			{
				validationResult.Value = userfavoriteDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var userfavoriteDA = new UserFavoriteDA())
			{
				validationResult.Value = userfavoriteDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<UserFavoriteEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<UserFavoriteEntity>();
		
			using (var userfavoriteDA = new UserFavoriteDA())
			{
				validationResult.Value = userfavoriteDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<UserFavoriteEntity> Update(UserFavoriteEntity userfavoriteEntity)
		{
			var validationResult = new ResultEntity<UserFavoriteEntity>();
		
			using (var userfavoriteDA = new UserFavoriteDA())
			{
				var resultUpdate = userfavoriteDA.Update(userfavoriteEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating UserFavorite!");
					return validationResult;
				}
		
				validationResult.Value = userfavoriteEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var userfavoriteDA = new UserFavoriteDA())
			{
				//var ids = new int[] { id };
				validationResult.Value = userfavoriteDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record UserFavorite with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}