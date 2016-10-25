
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class TrackingBL :BaseBL
    {
        public ResultEntity<TrackingEntity> Create(TrackingEntity trackingEntity)
        {
            var validationResult = new ResultEntity<TrackingEntity>();

			using (var trackingDA = new TrackingDA())
			{
				validationResult.Value = trackingDA.Create(trackingEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<TrackingEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<TrackingEntity>>();
		
			using (var trackingDA = new TrackingDA())
			{
				validationResult.Value = trackingDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var trackingDA = new TrackingDA())
			{
				validationResult.Value = trackingDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<TrackingEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<TrackingEntity>();
		
			using (var trackingDA = new TrackingDA())
			{
				validationResult.Value = trackingDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<TrackingEntity> Update(TrackingEntity trackingEntity)
		{
			var validationResult = new ResultEntity<TrackingEntity>();
		
			using (var trackingDA = new TrackingDA())
			{
				var resultUpdate = trackingDA.Update(trackingEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating Tracking!");
					return validationResult;
				}
		
				validationResult.Value = trackingEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var trackingDA = new TrackingDA())
			{
				//var ids = new int[] { id };
				validationResult.Value = trackingDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record Tracking with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}