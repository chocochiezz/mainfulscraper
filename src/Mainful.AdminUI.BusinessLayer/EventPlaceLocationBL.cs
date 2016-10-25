
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class EventPlaceLocationBL :BaseBL
    {
        public ResultEntity<EventPlaceLocationEntity> Create(EventPlaceLocationEntity eventplacelocationEntity)
        {
            var validationResult = new ResultEntity<EventPlaceLocationEntity>();

			using (var eventplacelocationDA = new EventPlaceLocationDA())
			{
				validationResult.Value = eventplacelocationDA.Create(eventplacelocationEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<EventPlaceLocationEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<EventPlaceLocationEntity>>();
		
			using (var eventplacelocationDA = new EventPlaceLocationDA())
			{
				validationResult.Value = eventplacelocationDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var eventplacelocationDA = new EventPlaceLocationDA())
			{
				validationResult.Value = eventplacelocationDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<EventPlaceLocationEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<EventPlaceLocationEntity>();
		
			using (var eventplacelocationDA = new EventPlaceLocationDA())
			{
				validationResult.Value = eventplacelocationDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<EventPlaceLocationEntity> Update(EventPlaceLocationEntity eventplacelocationEntity)
		{
			var validationResult = new ResultEntity<EventPlaceLocationEntity>();
		
			using (var eventplacelocationDA = new EventPlaceLocationDA())
			{
				var resultUpdate = eventplacelocationDA.Update(eventplacelocationEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating EventPlaceLocation!");
					return validationResult;
				}
		
				validationResult.Value = eventplacelocationEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var eventplacelocationDA = new EventPlaceLocationDA())
			{
				//var ids = new int[] { id };
				validationResult.Value = eventplacelocationDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record EventPlaceLocation with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}