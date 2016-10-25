
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class EventImageBL :BaseBL
    {
        public ResultEntity<EventImageEntity> Create(EventImageEntity eventimageEntity)
        {
            var validationResult = new ResultEntity<EventImageEntity>();

			using (var eventimageDA = new EventImageDA())
			{
				validationResult.Value = eventimageDA.Create(eventimageEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<EventImageEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<EventImageEntity>>();
		
			using (var eventimageDA = new EventImageDA())
			{
				validationResult.Value = eventimageDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var eventimageDA = new EventImageDA())
			{
				validationResult.Value = eventimageDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<EventImageEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<EventImageEntity>();
		
			using (var eventimageDA = new EventImageDA())
			{
				validationResult.Value = eventimageDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<EventImageEntity> Update(EventImageEntity eventimageEntity)
		{
			var validationResult = new ResultEntity<EventImageEntity>();
		
			using (var eventimageDA = new EventImageDA())
			{
				var resultUpdate = eventimageDA.Update(eventimageEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating EventImage!");
					return validationResult;
				}
		
				validationResult.Value = eventimageEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var eventimageDA = new EventImageDA())
			{
				//var ids = new int[] { id };
				validationResult.Value = eventimageDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record EventImage with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}