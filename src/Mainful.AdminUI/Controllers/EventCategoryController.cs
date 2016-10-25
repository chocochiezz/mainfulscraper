
using System;
using Mainful.AdminUI.BusinessLayer;
using Mainful.AdminUI.Shared.Entities;
using Mainful.AdminUI.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Mainful.AdminUI.Controllers
{
    public class EventCategoryController : BaseController
    {
		[HttpPost]
        public override JsonResultEntity Get([FromBody] DBParamEntity dbParamEntity)
        {
            EventCategoryBL eventcategoryBL = new EventCategoryBL();
            JsonResultEntity response = new JsonResultEntity();

            try
            {
                var result = eventcategoryBL.GetAll(dbParamEntity);

                if (result.HasWarning())
                {
                    response.Message = String.Join(",", result.Warning);
                    return response;
                }
				
                var dataFound = eventcategoryBL.GetTotalRows(dbParamEntity);
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
            EventCategoryBL eventcategoryBL = new EventCategoryBL();
            JsonResultEntity response = new JsonResultEntity();

            try
            {
                var result = eventcategoryBL.GetById(id);

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
		public JsonResultEntity Create([FromBody] EventCategoryEntity eventcategoryEntity)
        {
            EventCategoryBL eventcategoryBL = new EventCategoryBL();
            JsonResultEntity response = new JsonResultEntity();

            try
            {
                var result = eventcategoryBL.Create(eventcategoryEntity);

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
        public JsonResultEntity Update([FromBody] EventCategoryEntity eventcategoryEntity)
        {
            EventCategoryBL eventcategoryBL = new EventCategoryBL();
            JsonResultEntity response = new JsonResultEntity();
            try
            {
                var result = eventcategoryBL.Update(eventcategoryEntity);
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
            var eventcategoryBL = new EventCategoryBL();
            JsonResultEntity response = new JsonResultEntity();
            try
            {
                var result = eventcategoryBL.DeleteById(id);
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