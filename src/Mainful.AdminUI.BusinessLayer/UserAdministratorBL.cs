using System;
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using Mainful.AdminUI.Shared.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Mainful.AdminUI.BusinessLayer
{
    public class UserAdministratorBL :BaseBL
    {
        public ResultEntity<UserAdministratorEntity> Create(UserAdministratorEntity useradministratorEntity)
        {
            var validationResult = new ResultEntity<UserAdministratorEntity>();
            useradministratorEntity.Password = UtilityHelper.PasswordHash(useradministratorEntity.Password);

            using (var useradministratorDA = new UserAdministratorDA())
			{
                var userAdminList = useradministratorDA.GetByUserName(useradministratorEntity.UserName);

                if (userAdminList.Count() > 0)
                {
                    validationResult.Warning.Add("Username " + useradministratorEntity.UserName + " already exist");
                    return validationResult;
                }

                useradministratorEntity.CreatedDate = DateTime.Now;
                validationResult.Value = useradministratorDA.Create(useradministratorEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<UserAdministratorEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<UserAdministratorEntity>>();

            dbParamEntity.Filter.Add(new FilterDBParamEntity
            {
                Property = "IsDeleted",
                Operator = "eq",
                Value = "false"
            });

            using (var useradministratorDA = new UserAdministratorDA())
			{
				validationResult.Value = useradministratorDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var useradministratorDA = new UserAdministratorDA())
			{
				validationResult.Value = useradministratorDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<UserAdministratorEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<UserAdministratorEntity>();
		
			using (var useradministratorDA = new UserAdministratorDA())
			{
				validationResult.Value = useradministratorDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<UserAdministratorEntity> Update(UserAdministratorEntity useradministratorEntity)
		{
			var validationResult = new ResultEntity<UserAdministratorEntity>();
            //useradministratorEntity.Password = UtilityHelper.PasswordHash(useradministratorEntity.Password);

            using (var useradministratorDA = new UserAdministratorDA())
			{
                var userAdminList = useradministratorDA.GetByUserName(useradministratorEntity.UserName);

                var linq = (from x in userAdminList
                            where x.ID != useradministratorEntity.ID
                            select x).ToList<UserAdministratorEntity>();

                if (linq.Count() > 0)
                {
                    validationResult.Warning.Add("Username " + useradministratorEntity.UserName + " already exist");
                    return validationResult;
                }

                var isOldPassword = useradministratorDA.CheckPassword(useradministratorEntity.ID, useradministratorEntity.Password);

                if(isOldPassword==null)
                    useradministratorEntity.Password = UtilityHelper.PasswordHash(useradministratorEntity.Password);

                var resultUpdate = useradministratorDA.Update(useradministratorEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating UserAdministrator!");
					return validationResult;
				}
		
				validationResult.Value = useradministratorEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var useradministratorDA = new UserAdministratorDA())
			{
				////var ids = new int[] { id };
				validationResult.Value = useradministratorDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record UserAdministrator with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}