using System;
using Mainful.AdminUI.BusinessLayer;
using Mainful.AdminUI.Shared.Entities;
using Mainful.AdminUI.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Mainful.AdminUI.Controllers
{
    public class LoginController : BaseController
    {
        [HttpPost]
        public JsonResultEntity Login(LoginEntity loginentity)
        {
            LoginBL loginBL = new LoginBL();
            JsonResultEntity response = new JsonResultEntity();
            
            try
            {
                var result = loginBL.Get(loginentity);
                
                if (result.Count == 0)
                {
                    response.Success = false;
                    response.Message = "Username or Password is incorrect";
                    return response;
                }

                HttpContext.Session.SetString("username", result[0].UserName);
                HttpContext.Session.SetInt32("userid", result[0].UserID);
                HttpContext.Session.SetString("groupname", result[0].GroupName);
                HttpContext.Session.SetInt32("groupid", result[0].GroupID);

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                LoggerHelper.Error(ex);
            }

            return response;
        }

        [HttpPost]
        public JsonResultEntity Logout()
        {
            JsonResultEntity response = new JsonResultEntity();

            try
            {                   
                HttpContext.Session.Clear();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                LoggerHelper.Error(ex);
            }

            return response;
        }

        [HttpPost]
        public JsonResultEntity CheckSession()
        {
            JsonResultEntity response = new JsonResultEntity();

            try
            {
                var username = GetUserName();

                if (!string.IsNullOrEmpty(username))
                    response.Success = true;
                else
                    response.Success = false;
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
