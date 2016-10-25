
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class AppMessageBL :BaseBL
    {
        public ResultEntity<AppMessageEntity> Create(AppMessageEntity appmessageEntity)
        {
            var validationResult = new ResultEntity<AppMessageEntity>();

			using (var appmessageDA = new AppMessageDA())
			{
				validationResult.Value = appmessageDA.Create(appmessageEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<AppMessageEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<AppMessageEntity>>();
		
			using (var appmessageDA = new AppMessageDA())
			{
				validationResult.Value = appmessageDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var appmessageDA = new AppMessageDA())
			{
				validationResult.Value = appmessageDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<AppMessageEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<AppMessageEntity>();
		
			using (var appmessageDA = new AppMessageDA())
			{
				validationResult.Value = appmessageDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<AppMessageEntity> Update(AppMessageEntity appmessageEntity)
		{
			var validationResult = new ResultEntity<AppMessageEntity>();
		
			using (var appmessageDA = new AppMessageDA())
			{
				var resultUpdate = appmessageDA.Update(appmessageEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating AppMessage!");
					return validationResult;
				}
		
				validationResult.Value = appmessageEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var appmessageDA = new AppMessageDA())
			{
				//var ids = new int[] { id };
				validationResult.Value = appmessageDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record AppMessage with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}