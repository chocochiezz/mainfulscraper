using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Mainful.AdminUI.Shared.Entities;
using Microsoft.AspNetCore.Http;

namespace Mainful.AdminUI.Shared.Helpers
{
    public class ConfigHelper
    {
        // use IConfiguration for more comprehensive configuration multi-environment
        public static IConfiguration Configuration;

        // use Dictionary if you want to make this class more independent
        //public static Dictionary<string, object> Configuration = new Dictionary<string, object>();

        public static string GetDefaultConnection()
        {
            return Configuration.GetConnectionString("defaultConnection");
        }

        public static string Get(string key)
        {            
            return Configuration.GetSection(key).Value;
        }       
    }
}
