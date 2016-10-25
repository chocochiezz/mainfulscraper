using System;
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class EventOrganizerBL :BaseBL
    {
        public ResultEntity<EventOrganizerEntity> Create(EventOrganizerEntity eventorganizerEntity)
        {
            var validationResult = new ResultEntity<EventOrganizerEntity>();

			using (var eventorganizerDA = new EventOrganizerDA())
			{
				validationResult.Value = eventorganizerDA.Create(eventorganizerEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<EventOrganizerEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<EventOrganizerEntity>>();
		
			using (var eventorganizerDA = new EventOrganizerDA())
			{
				validationResult.Value = eventorganizerDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var eventorganizerDA = new EventOrganizerDA())
			{
				validationResult.Value = eventorganizerDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<EventOrganizerEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<EventOrganizerEntity>();
		
			using (var eventorganizerDA = new EventOrganizerDA())
			{
				validationResult.Value = eventorganizerDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<EventOrganizerEntity> Update(EventOrganizerEntity eventorganizerEntity)
		{
			var validationResult = new ResultEntity<EventOrganizerEntity>();
            eventorganizerEntity.ModifiedDate = DateTime.Now;

            using (var eventorganizerDA = new EventOrganizerDA())
			{
				var resultUpdate = eventorganizerDA.Update(eventorganizerEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating EventOrganizer!");
					return validationResult;
				}
		
				validationResult.Value = eventorganizerEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var eventorganizerDA = new EventOrganizerDA())
			{
				//var ids = new int[] { id };
				validationResult.Value = eventorganizerDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record EventOrganizer with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}