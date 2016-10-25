
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class UserLoginBL :BaseBL
    {
        public ResultEntity<UserLoginEntity> Create(UserLoginEntity userloginEntity)
        {
            var validationResult = new ResultEntity<UserLoginEntity>();

			using (var userloginDA = new UserLoginDA())
			{
				validationResult.Value = userloginDA.Create(userloginEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<UserLoginEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<UserLoginEntity>>();
		
			using (var userloginDA = new UserLoginDA())
			{
				validationResult.Value = userloginDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var userloginDA = new UserLoginDA())
			{
				validationResult.Value = userloginDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<UserLoginEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<UserLoginEntity>();
		
			using (var userloginDA = new UserLoginDA())
			{
				validationResult.Value = userloginDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<UserLoginEntity> Update(UserLoginEntity userloginEntity)
		{
			var validationResult = new ResultEntity<UserLoginEntity>();
		
			using (var userloginDA = new UserLoginDA())
			{
				var resultUpdate = userloginDA.Update(userloginEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating UserLogin!");
					return validationResult;
				}
		
				validationResult.Value = userloginEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var userloginDA = new UserLoginDA())
			{
				//var ids = new int[] { id };
				validationResult.Value = userloginDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record UserLogin with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}