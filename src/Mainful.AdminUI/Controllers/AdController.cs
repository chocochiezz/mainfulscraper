using System;
using Mainful.AdminUI.BusinessLayer;
using Mainful.AdminUI.Shared.Entities;
using Mainful.AdminUI.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace Mainful.AdminUI.Controllers
{
    public class AdController : BaseController
    {
        [HttpPost]
        public override JsonResultEntity Get([FromBody] DBParamEntity dbParamEntity)
        {
            AdBL adBL = new AdBL();
            JsonResultEntity response = new JsonResultEntity();

            try
            {
                var result = adBL.GetAll(dbParamEntity);

                if (result.HasWarning())
                {
                    response.Message = String.Join(",", result.Warning);
                    return response;
                }

                var dataFound = adBL.GetTotalRows(dbParamEntity);
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
            AdBL adBL = new AdBL();
            JsonResultEntity response = new JsonResultEntity();

            try
            {
                var result = adBL.GetById(id);

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
        public JsonResultEntity Create([FromBody] AdEntity adEntity)
        {
            AdBL adBL = new AdBL();
            JsonResultEntity response = new JsonResultEntity();

            if (UtilityHelper.ModelBindingValidator(ModelState).Length > 0)
            {
                response.Message = UtilityHelper.ModelBindingValidator(ModelState);
                return response;
            }

            try
            {
                var result = adBL.Create(adEntity);

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
        public JsonResultEntity Update([FromBody] AdEntity adEntity)
        {
            AdBL adBL = new AdBL();
            JsonResultEntity response = new JsonResultEntity();

            if (UtilityHelper.ModelBindingValidator(ModelState).Length > 0)
            {
                response.Message = UtilityHelper.ModelBindingValidator(ModelState);
                return response;
            }

            try
            {
                var result = adBL.Update(adEntity);
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
            var adBL = new AdBL();
            JsonResultEntity response = new JsonResultEntity();
            try
            {
                var result = adBL.DeleteById(id);
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

        [HttpGet]
        public JsonResultEntity GetContentID(string contentSource)
        {
            AdBL adBL = new AdBL();
            JsonResultEntity response = new JsonResultEntity();            

            try
            {
                var result = new ResultEntity<IEnumerable<ContentEntity>>();

                switch (contentSource)
                {
                    case "event":
                        result = adBL.GetContentEvent();
                        break;
                    case "seminar":
                        result = adBL.GetContentSeminar();
                        break;
                    case "promo":
                        result = adBL.GetContentPromo();
                        break;
                }

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
    }
}