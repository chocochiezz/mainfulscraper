
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class ClientBL :BaseBL
    {
        public ResultEntity<ClientEntity> Create(ClientEntity clientEntity)
        {
            var validationResult = new ResultEntity<ClientEntity>();

			using (var clientDA = new ClientDA())
			{
				validationResult.Value = clientDA.Create(clientEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<ClientEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<ClientEntity>>();
		
			using (var clientDA = new ClientDA())
			{
				validationResult.Value = clientDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var clientDA = new ClientDA())
			{
				validationResult.Value = clientDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<ClientEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<ClientEntity>();
		
			using (var clientDA = new ClientDA())
			{
				validationResult.Value = clientDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<ClientEntity> Update(ClientEntity clientEntity)
		{
			var validationResult = new ResultEntity<ClientEntity>();
		
			using (var clientDA = new ClientDA())
			{
				var resultUpdate = clientDA.Update(clientEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating Client!");
					return validationResult;
				}
		
				validationResult.Value = clientEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var clientDA = new ClientDA())
			{
				//var ids = new int[] { id };
				validationResult.Value = clientDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record Client with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}