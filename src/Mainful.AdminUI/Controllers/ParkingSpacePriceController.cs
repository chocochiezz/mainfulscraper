
using System;
using Mainful.AdminUI.BusinessLayer;
using Mainful.AdminUI.Shared.Entities;
using Mainful.AdminUI.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Mainful.AdminUI.Controllers
{
    public class ParkingSpacePriceController : BaseController
    {
		[HttpPost]
        public override JsonResultEntity Get([FromBody] DBParamEntity dbParamEntity)
        {
            ParkingSpacePriceBL parkingspacepriceBL = new ParkingSpacePriceBL();
            JsonResultEntity response = new JsonResultEntity();

            try
            {
                var result = parkingspacepriceBL.GetAll(dbParamEntity);

                if (result.HasWarning())
                {
                    response.Message = String.Join(",", result.Warning);
                    return response;
                }
				
                var dataFound = parkingspacepriceBL.GetTotalRows(dbParamEntity);
                var totalPages = Convert.ToInt32(Math.Ceiling(dataFound.Value / Convert.ToDouble(dbParamEntity.Limit)));

                response.Success = true;
                response.Data = result.Value;
                response.MetaInfo = new MetaInfoEntity
                {
                    DataFound = dataFound.Value,
                    DataPerPage = dbParamEntity.Limit,
                    Page = dbParamEntity.Page,
                    TotalPages = totalPages == 0 ? 1 : totalPages
                };				
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                LoggerHelper.Error(ex);
            }

            return response;            
        }

        [HttpGet]
		public JsonResultEntity GetById(int id)
        {
            ParkingSpacePriceBL parkingspacepriceBL = new ParkingSpacePriceBL();
            JsonResultEntity response = new JsonResultEntity();

            try
            {
                var result = parkingspacepriceBL.GetById(id);

                if (result.HasWarning())
                {
                    response.Message = String.Join(",", result.Warning);
                    return response;
                }

                response.Success = true;
                response.Data = result.Value;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                LoggerHelper.Error(ex);
            }

            return response;            
        }

        [HttpPost]
		public JsonResultEntity Create([FromBody] ParkingSpacePriceEntity parkingspacepriceEntity)
        {
            ParkingSpacePriceBL parkingspacepriceBL = new ParkingSpacePriceBL();
            JsonResultEntity response = new JsonResultEntity();

            try
            {
                var result = parkingspacepriceBL.Create(parkingspacepriceEntity);

                if (result.HasWarning())
                {
                    response.Message = String.Join(",", result.Warning);
                    return response;
                }

                response.Success = true;
                response.Data = result.Value;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                LoggerHelper.Error(ex);
            }

            return response;
        }

        [HttpPost]
        public JsonResultEntity Update([FromBody] ParkingSpacePriceEntity parkingspacepriceEntity)
        {
            ParkingSpacePriceBL parkingspacepriceBL = new ParkingSpacePriceBL();
            JsonResultEntity response = new JsonResultEntity();
            try
            {
                var result = parkingspacepriceBL.Update(parkingspacepriceEntity);
                if (result.HasWarning() || result.Value == null)
                {
                    response.Message = String.Join(",", result.Warning);
                    return response;
                }
                response.Success = true;
                response.Data = result.Value;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                LoggerHelper.Error(e);
            }
            return response;
        }

        [HttpGet]
        public JsonResultEntity Delete(int id)
        {
            var parkingspacepriceBL = new ParkingSpacePriceBL();
            JsonResultEntity response = new JsonResultEntity();
            try
            {
                var result = parkingspacepriceBL.DeleteById(id);
                if (result.HasWarning())
                {
                    response.Message = String.Join(",", result.Warning);
                    return response;
                }

                response.Success = true;
                response.Data = result.Value;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                LoggerHelper.Error(e);
            }
            return response;
        }
    }
}