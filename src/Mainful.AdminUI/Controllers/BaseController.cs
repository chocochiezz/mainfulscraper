using Mainful.AdminUI.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Mainful.AdminUI.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Get records with specified Page (param1) and Limit per page (param2)
        /// </summary>
        /// <param name="page">Page.</param>
        /// <param name="limit">Limit per page.</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResultEntity Get(int page = 1, int limit = 30)
        {
            var dbParamEntity = ExtractQueryString(ref page, ref limit);

            return Get(dbParamEntity);
        }

        /// <summary>
        /// Gets the specified database parameter entity.
        /// </summary>
        /// <param name="dbParamEntity">The database parameter entity.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        [HttpPost]
        public virtual JsonResultEntity Get(DBParamEntity dbParamEntity)
        {
            throw new NotImplementedException();
        }
        // GENERIC CRUD will be coded here....

        /// <summary>
        /// Extracts the query string.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <returns></returns>
        protected DBParamEntity ExtractQueryString(ref int param1, ref int param2)
        {
            var queryString = Request.Query;
            var page = queryString.FirstOrDefault(w => w.Key.Equals("page"));
            var limit = queryString.FirstOrDefault(w => w.Key.Equals("limit"));
            var filter = queryString.FirstOrDefault(w => w.Key.Equals("filter"));
            var sort = queryString.FirstOrDefault(w => w.Key.Equals("sort"));
            var group = queryString.FirstOrDefault(w => w.Key.Equals("group"));

            if (String.IsNullOrEmpty(page.Value) == false)
            {
                int.TryParse(page.Value, out param1);
            }

            if (String.IsNullOrEmpty(limit.Value) == false)
            {
                int.TryParse(limit.Value, out param2);
            }

            var dbParamEntity = new DBParamEntity
            {
                Page = param1,
                Limit = param2
            };

            if (String.IsNullOrEmpty(filter.Value) == false)
            {
                dbParamEntity.Filter = JsonConvert.DeserializeObject<List<FilterDBParamEntity>>(filter.Value);
            }

            if (String.IsNullOrEmpty(sort.Value) == false)
            {
                dbParamEntity.Sort = JsonConvert.DeserializeObject<List<SortDBParamEntity>>(sort.Value);
            }

            //if (String.IsNullOrEmpty(group.Value) == false)
            //{
            //	//dbParamEntity.Group = JsonConvert.DeserializeObject<List<GroupDBParamEntity>>(group.Value);

            //	// this is special behavior of ExtJS grouping feature on the Gridpanel
            //	// so need to handle with care, to make it gridpanel grouping working fine
            //	// -----------------------------------------------------------------------------------
            //	var groupBy = JsonConvert.DeserializeObject<GroupDBParamEntity>(group.Value);
            //	// do not add to sort list when the property already on the list	
            //	if (dbParamEntity.Sort.Count(w => w.Property.Equals(groupBy.Property)) == 0)
            //	{
            //		dbParamEntity.Sort.Add(new SortDBParamEntity
            //		{
            //			Property = groupBy.Property,
            //			Direction = groupBy.Direction
            //		});
            //	}
            //	dbParamEntity.Sort.Reverse();
            //	// -----------------------------------------------------------------------------------
            //}

            //dbParamEntity.QueryStringEntity = UtilityHelper.GetFromQueryString<PredefinedQueryStringEntity>();

            return dbParamEntity;
        }

        protected string ExtractQueryString(string key)
        {
            var queryString = Request.Query; //GetQueryNameValuePairs();
            var value = queryString.FirstOrDefault(w => w.Key.Equals(key)).Value;

            return value;
        }

        protected string GetUserName()
        {
            return Convert.ToString(HttpContext.Session.GetString("username")); ;
        }

        protected string GetGroupName()
        {
            return Convert.ToString(HttpContext.Session.GetString("groupname")); ;
        }

        protected int GetUserID()
        {
            var ret = HttpContext.Session.GetInt32("userid") == null ? 0 : Convert.ToInt32(HttpContext.Session.GetInt32("userid"));
            return ret;
        }

        protected int GetGroupID()
        {
            var ret = HttpContext.Session.GetInt32("groupid") == null ? 0 : Convert.ToInt32(HttpContext.Session.GetInt32("groupid"));
            return ret;
        }
    }
}
