
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class SchedulerSessionBL :BaseBL
    {
        public ResultEntity<SchedulerSessionEntity> Create(SchedulerSessionEntity schedulersessionEntity)
        {
            var validationResult = new ResultEntity<SchedulerSessionEntity>();

			using (var schedulersessionDA = new SchedulerSessionDA())
			{
				validationResult.Value = schedulersessionDA.Create(schedulersessionEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<SchedulerSessionEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<SchedulerSessionEntity>>();
		
			using (var schedulersessionDA = new SchedulerSessionDA())
			{
				validationResult.Value = schedulersessionDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var schedulersessionDA = new SchedulerSessionDA())
			{
				validationResult.Value = schedulersessionDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<SchedulerSessionEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<SchedulerSessionEntity>();
		
			using (var schedulersessionDA = new SchedulerSessionDA())
			{
				validationResult.Value = schedulersessionDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<SchedulerSessionEntity> Update(SchedulerSessionEntity schedulersessionEntity)
		{
			var validationResult = new ResultEntity<SchedulerSessionEntity>();
		
			using (var schedulersessionDA = new SchedulerSessionDA())
			{
				var resultUpdate = schedulersessionDA.Update(schedulersessionEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating SchedulerSession!");
					return validationResult;
				}
		
				validationResult.Value = schedulersessionEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var schedulersessionDA = new SchedulerSessionDA())
			{
				//var ids = new int[] { id };
				validationResult.Value = schedulersessionDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record SchedulerSession with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}