
using System;
using Mainful.AdminUI.BusinessLayer;
using Mainful.AdminUI.Shared.Entities;
using Mainful.AdminUI.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Mainful.AdminUI.Controllers
{
    public class UserAdminParkingController : BaseController
    {
		[HttpPost]
        public override JsonResultEntity Get([FromBody] DBParamEntity dbParamEntity)
        {
            UserAdminParkingBL useradminparkingBL = new UserAdminParkingBL();
            JsonResultEntity response = new JsonResultEntity();

            try
            {
                var result = useradminparkingBL.GetAll(dbParamEntity);

                if (result.HasWarning())
                {
                    response.Message = String.Join(",", result.Warning);
                    return response;
                }
				
                var dataFound = useradminparkingBL.GetTotalRows(dbParamEntity);
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
            UserAdminParkingBL useradminparkingBL = new UserAdminParkingBL();
            JsonResultEntity response = new JsonResultEntity();

            try
            {
                var result = useradminparkingBL.GetById(id);

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
		public JsonResultEntity Create([FromBody] UserAdminParkingEntity useradminparkingEntity)
        {
            UserAdminParkingBL useradminparkingBL = new UserAdminParkingBL();
            JsonResultEntity response = new JsonResultEntity();

            try
            {
                var result = useradminparkingBL.Create(useradminparkingEntity);

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
        public JsonResultEntity Update([FromBody] UserAdminParkingEntity useradminparkingEntity)
        {
            UserAdminParkingBL useradminparkingBL = new UserAdminParkingBL();
            JsonResultEntity response = new JsonResultEntity();
            try
            {
                var result = useradminparkingBL.Update(useradminparkingEntity);
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
            var useradminparkingBL = new UserAdminParkingBL();
            JsonResultEntity response = new JsonResultEntity();
            try
            {
                var result = useradminparkingBL.DeleteById(id);
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