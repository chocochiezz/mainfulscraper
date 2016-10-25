
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class EventCategoryBL :BaseBL
    {
        public ResultEntity<EventCategoryEntity> Create(EventCategoryEntity eventcategoryEntity)
        {
            var validationResult = new ResultEntity<EventCategoryEntity>();

			using (var eventcategoryDA = new EventCategoryDA())
			{
				validationResult.Value = eventcategoryDA.Create(eventcategoryEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<EventCategoryEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<EventCategoryEntity>>();
		
			using (var eventcategoryDA = new EventCategoryDA())
			{
				validationResult.Value = eventcategoryDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var eventcategoryDA = new EventCategoryDA())
			{
				validationResult.Value = eventcategoryDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<EventCategoryEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<EventCategoryEntity>();
		
			using (var eventcategoryDA = new EventCategoryDA())
			{
				validationResult.Value = eventcategoryDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<EventCategoryEntity> Update(EventCategoryEntity eventcategoryEntity)
		{
			var validationResult = new ResultEntity<EventCategoryEntity>();
		
			using (var eventcategoryDA = new EventCategoryDA())
			{
				var resultUpdate = eventcategoryDA.Update(eventcategoryEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating EventCategory!");
					return validationResult;
				}
		
				validationResult.Value = eventcategoryEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var eventcategoryDA = new EventCategoryDA())
			{
				//var ids = new int[] { id };
				validationResult.Value = eventcategoryDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record EventCategory with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}