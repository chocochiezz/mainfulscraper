using System;
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class UserAdminParkingBL :BaseBL
    {
        public ResultEntity<UserAdminParkingEntity> Create(UserAdminParkingEntity useradminparkingEntity)
        {
            var validationResult = new ResultEntity<UserAdminParkingEntity>();
            useradminparkingEntity.CreatedDate = DateTime.Now;
            
			using (var useradminparkingDA = new UserAdminParkingDA())
			{
				validationResult.Value = useradminparkingDA.Create(useradminparkingEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<UserAdminParkingEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<UserAdminParkingEntity>>();
		
			using (var useradminparkingDA = new UserAdminParkingDA())
			{
				validationResult.Value = useradminparkingDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var useradminparkingDA = new UserAdminParkingDA())
			{
				validationResult.Value = useradminparkingDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<UserAdminParkingEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<UserAdminParkingEntity>();
		
			using (var useradminparkingDA = new UserAdminParkingDA())
			{
				validationResult.Value = useradminparkingDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<UserAdminParkingEntity> Update(UserAdminParkingEntity useradminparkingEntity)
		{
			var validationResult = new ResultEntity<UserAdminParkingEntity>();
		
			using (var useradminparkingDA = new UserAdminParkingDA())
			{
				var resultUpdate = useradminparkingDA.Update(useradminparkingEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating UserAdminParking!");
					return validationResult;
				}
		
				validationResult.Value = useradminparkingEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var useradminparkingDA = new UserAdminParkingDA())
			{
				//var ids = new int[] { id };
				validationResult.Value = useradminparkingDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record UserAdminParking with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}