
using System;
using Mainful.AdminUI.BusinessLayer;
using Mainful.AdminUI.Shared.Entities;
using Mainful.AdminUI.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Mainful.AdminUI.Controllers
{
    public class UserInterestController : BaseController
    {
		[HttpPost]
        public override JsonResultEntity Get([FromBody] DBParamEntity dbParamEntity)
        {
            UserInterestBL userinterestBL = new UserInterestBL();
            JsonResultEntity response = new JsonResultEntity();

            try
            {
                var result = userinterestBL.GetAll(dbParamEntity);

                if (result.HasWarning())
                {
                    response.Message = String.Join(",", result.Warning);
                    return response;
                }
				
                var dataFound = userinterestBL.GetTotalRows(dbParamEntity);
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
            UserInterestBL userinterestBL = new UserInterestBL();
            JsonResultEntity response = new JsonResultEntity();

            try
            {
                var result = userinterestBL.GetById(id);

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
		public JsonResultEntity Create([FromBody] UserInterestEntity userinterestEntity)
        {
            UserInterestBL userinterestBL = new UserInterestBL();
            JsonResultEntity response = new JsonResultEntity();

            try
            {
                var result = userinterestBL.Create(userinterestEntity);

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
        public JsonResultEntity Update([FromBody] UserInterestEntity userinterestEntity)
        {
            UserInterestBL userinterestBL = new UserInterestBL();
            JsonResultEntity response = new JsonResultEntity();
            try
            {
                var result = userinterestBL.Update(userinterestEntity);
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
            var userinterestBL = new UserInterestBL();
            JsonResultEntity response = new JsonResultEntity();
            try
            {
                var result = userinterestBL.DeleteById(id);
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