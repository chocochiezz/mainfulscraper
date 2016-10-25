using System;
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class EventBL :BaseBL
    {
        public ResultEntity<EventEntity> Create(EventEntity eventEntity)
        {
            var validationResult = new ResultEntity<EventEntity>();
            eventEntity.CreatedDate = DateTime.Now;

            using (var eventDA = new EventDA())
			{
				validationResult.Value = eventDA.Create(eventEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<EventEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<EventEntity>>();
		
			using (var eventDA = new EventDA())
			{
				validationResult.Value = eventDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var eventDA = new EventDA())
			{
				validationResult.Value = eventDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<EventEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<EventEntity>();
		
			using (var eventDA = new EventDA())
			{
				validationResult.Value = eventDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<EventEntity> Update(EventEntity eventEntity)
		{
			var validationResult = new ResultEntity<EventEntity>();
            eventEntity.ModifiedDate = DateTime.Now;

            using (var eventDA = new EventDA())
			{
				var resultUpdate = eventDA.Update(eventEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating Event!");
					return validationResult;
				}
		
				validationResult.Value = eventEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var eventDA = new EventDA())
			{
				//var ids = new int[] { id };
				validationResult.Value = eventDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record Event with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}