using System;
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class PlaceLocationBL :BaseBL
    {
        public ResultEntity<PlaceLocationEntity> Create(PlaceLocationEntity placelocationEntity)
        {
            var validationResult = new ResultEntity<PlaceLocationEntity>();
            placelocationEntity.CreatedDate = DateTime.Now;

            using (var placelocationDA = new PlaceLocationDA())
			{
				validationResult.Value = placelocationDA.Create(placelocationEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<PlaceLocationEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<PlaceLocationEntity>>();
		
			using (var placelocationDA = new PlaceLocationDA())
			{
				validationResult.Value = placelocationDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var placelocationDA = new PlaceLocationDA())
			{
				validationResult.Value = placelocationDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<PlaceLocationEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<PlaceLocationEntity>();
		
			using (var placelocationDA = new PlaceLocationDA())
			{
				validationResult.Value = placelocationDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<PlaceLocationEntity> Update(PlaceLocationEntity placelocationEntity)
		{
			var validationResult = new ResultEntity<PlaceLocationEntity>();
            placelocationEntity.ModifiedDate = DateTime.Now;

            using (var placelocationDA = new PlaceLocationDA())
			{
				var resultUpdate = placelocationDA.Update(placelocationEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating PlaceLocation!");
					return validationResult;
				}
		
				validationResult.Value = placelocationEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var placelocationDA = new PlaceLocationDA())
			{
				//var ids = new int[] { id };
				validationResult.Value = placelocationDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record PlaceLocation with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}