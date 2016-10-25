
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class EmailQueueBL :BaseBL
    {
        public ResultEntity<EmailQueueEntity> Create(EmailQueueEntity emailqueueEntity)
        {
            var validationResult = new ResultEntity<EmailQueueEntity>();

			using (var emailqueueDA = new EmailQueueDA())
			{
				validationResult.Value = emailqueueDA.Create(emailqueueEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<EmailQueueEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<EmailQueueEntity>>();
		
			using (var emailqueueDA = new EmailQueueDA())
			{
				validationResult.Value = emailqueueDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var emailqueueDA = new EmailQueueDA())
			{
				validationResult.Value = emailqueueDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<EmailQueueEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<EmailQueueEntity>();
		
			using (var emailqueueDA = new EmailQueueDA())
			{
				validationResult.Value = emailqueueDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<EmailQueueEntity> Update(EmailQueueEntity emailqueueEntity)
		{
			var validationResult = new ResultEntity<EmailQueueEntity>();
		
			using (var emailqueueDA = new EmailQueueDA())
			{
				var resultUpdate = emailqueueDA.Update(emailqueueEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating EmailQueue!");
					return validationResult;
				}
		
				validationResult.Value = emailqueueEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var emailqueueDA = new EmailQueueDA())
			{
				//var ids = new int[] { id };
				validationResult.Value = emailqueueDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record EmailQueue with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}