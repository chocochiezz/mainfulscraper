using System;
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class ParkingSpacePriceBL :BaseBL
    {
        public ResultEntity<ParkingSpacePriceEntity> Create(ParkingSpacePriceEntity parkingspacepriceEntity)
        {
            var validationResult = new ResultEntity<ParkingSpacePriceEntity>();
            parkingspacepriceEntity.CreatedDate = DateTime.Now;

            using (var parkingspacepriceDA = new ParkingSpacePriceDA())
			{
				validationResult.Value = parkingspacepriceDA.Create(parkingspacepriceEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<ParkingSpacePriceEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<ParkingSpacePriceEntity>>();
		
			using (var parkingspacepriceDA = new ParkingSpacePriceDA())
			{
				validationResult.Value = parkingspacepriceDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var parkingspacepriceDA = new ParkingSpacePriceDA())
			{
				validationResult.Value = parkingspacepriceDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<ParkingSpacePriceEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<ParkingSpacePriceEntity>();
		
			using (var parkingspacepriceDA = new ParkingSpacePriceDA())
			{
				validationResult.Value = parkingspacepriceDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<ParkingSpacePriceEntity> Update(ParkingSpacePriceEntity parkingspacepriceEntity)
		{
			var validationResult = new ResultEntity<ParkingSpacePriceEntity>();
            parkingspacepriceEntity.ModifiedDate = DateTime.Now;

            using (var parkingspacepriceDA = new ParkingSpacePriceDA())
			{
				var resultUpdate = parkingspacepriceDA.Update(parkingspacepriceEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating ParkingSpacePrice!");
					return validationResult;
				}
		
				validationResult.Value = parkingspacepriceEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var parkingspacepriceDA = new ParkingSpacePriceDA())
			{
				//var ids = new int[] { id };
				validationResult.Value = parkingspacepriceDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record ParkingSpacePrice with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}