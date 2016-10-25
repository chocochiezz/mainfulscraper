using System;
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using Mainful.AdminUI.Shared.Helpers;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class UserProfileBL :BaseBL
    {
        public ResultEntity<UserProfileEntity> Create(UserProfileEntity userprofileEntity)
        {
            var validationResult = new ResultEntity<UserProfileEntity>();

            userprofileEntity.PasswordHash = UtilityHelper.PasswordHash(userprofileEntity.PasswordHash);
            userprofileEntity.CreatedDate = DateTime.Now;

            using (var userprofileDA = new UserProfileDA())
			{
				validationResult.Value = userprofileDA.Create(userprofileEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<UserProfileEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<UserProfileEntity>>();

            dbParamEntity.Filter.Add(new FilterDBParamEntity
            {
                Property = "IsDeleted",
                Operator = "eq",
                Value = "false"
            });

            using (var userprofileDA = new UserProfileDA())
			{
				validationResult.Value = userprofileDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var userprofileDA = new UserProfileDA())
			{
				validationResult.Value = userprofileDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<UserProfileEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<UserProfileEntity>();
		
			using (var userprofileDA = new UserProfileDA())
			{
				validationResult.Value = userprofileDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<UserProfileEntity> Update(UserProfileEntity userprofileEntity)
		{
			var validationResult = new ResultEntity<UserProfileEntity>();
            userprofileEntity.ModifiedDate = DateTime.Now;

            using (var userprofileDA = new UserProfileDA())
			{
				var resultUpdate = userprofileDA.Update(userprofileEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating UserProfile!");
					return validationResult;
				}
		
				validationResult.Value = userprofileEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var userprofileDA = new UserProfileDA())
			{
				////var ids = new int[] { id };
				validationResult.Value = userprofileDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record UserProfile with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}