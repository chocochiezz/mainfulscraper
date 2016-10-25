
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class ParkingSpaceBL :BaseBL
    {
        public ResultEntity<ParkingSpaceEntity> Create(ParkingSpaceEntity parkingspaceEntity)
        {
            var validationResult = new ResultEntity<ParkingSpaceEntity>();

			using (var parkingspaceDA = new ParkingSpaceDA())
			{
				validationResult.Value = parkingspaceDA.Create(parkingspaceEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<ParkingSpaceEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<ParkingSpaceEntity>>();
		
			using (var parkingspaceDA = new ParkingSpaceDA())
			{
				validationResult.Value = parkingspaceDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var parkingspaceDA = new ParkingSpaceDA())
			{
				validationResult.Value = parkingspaceDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<ParkingSpaceEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<ParkingSpaceEntity>();
		
			using (var parkingspaceDA = new ParkingSpaceDA())
			{
				validationResult.Value = parkingspaceDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<ParkingSpaceEntity> Update(ParkingSpaceEntity parkingspaceEntity)
		{
			var validationResult = new ResultEntity<ParkingSpaceEntity>();
		
			using (var parkingspaceDA = new ParkingSpaceDA())
			{
				var resultUpdate = parkingspaceDA.Update(parkingspaceEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating ParkingSpace!");
					return validationResult;
				}
		
				validationResult.Value = parkingspaceEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var parkingspaceDA = new ParkingSpaceDA())
			{
				var ids = new int[] { id };
				validationResult.Value = parkingspaceDA.Delete(ids);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record ParkingSpace with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}