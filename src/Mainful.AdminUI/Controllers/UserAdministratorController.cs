
using System;
using Mainful.AdminUI.BusinessLayer;
using Mainful.AdminUI.Shared.Entities;
using Mainful.AdminUI.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Mainful.AdminUI.Controllers
{
    public class UserAdministratorController : BaseController
    {
		[HttpPost]
        public override JsonResultEntity Get([FromBody] DBParamEntity dbParamEntity)
        {
            UserAdministratorBL useradministratorBL = new UserAdministratorBL();
            JsonResultEntity response = new JsonResultEntity();

            try
            {
                var result = useradministratorBL.GetAll(dbParamEntity);

                if (result.HasWarning())
                {
                    response.Message = String.Join(",", result.Warning);
                    return response;
                }
				
                var dataFound = useradministratorBL.GetTotalRows(dbParamEntity);
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
            UserAdministratorBL useradministratorBL = new UserAdministratorBL();
            JsonResultEntity response = new JsonResultEntity();

            try
            {
                var result = useradministratorBL.GetById(id);

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
		public JsonResultEntity Create([FromBody] UserAdministratorEntity useradministratorEntity)
        {
            UserAdministratorBL useradministratorBL = new UserAdministratorBL();
            JsonResultEntity response = new JsonResultEntity();

            try
            {
                useradministratorEntity.CreatedBy = GetUserID();                

                var result = useradministratorBL.Create(useradministratorEntity);

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
        public JsonResultEntity Update([FromBody] UserAdministratorEntity useradministratorEntity)
        {
            UserAdministratorBL useradministratorBL = new UserAdministratorBL();
            JsonResultEntity response = new JsonResultEntity();
            try
            {
                useradministratorEntity.ModifiedBy = GetUserID();                

                var result = useradministratorBL.Update(useradministratorEntity);
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
            var useradministratorBL = new UserAdministratorBL();
            JsonResultEntity response = new JsonResultEntity();
            try
            {
                if (id == 1)
                {
                    response.Message = "Cannot delete user admin";
                    response.Success = false;

                    return response;
                }

                var result = useradministratorBL.DeleteById(id);
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