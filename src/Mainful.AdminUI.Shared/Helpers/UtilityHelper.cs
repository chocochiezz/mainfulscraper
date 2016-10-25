using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace Mainful.AdminUI.Shared.Helpers
{
    public class UtilityHelper
    {
        public static string PasswordHashSalt
        {
            get
            {
                return "{sUp3r!d00p3r#te4m}";
            }
        }

        public static string PasswordHash(string plain)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(plain + PasswordHashSalt);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static string ModelBindingValidator(ModelStateDictionary modelState)
        {
            var message = string.Empty;

            if (!modelState.IsValid)
            {
                var error = modelState.SelectMany(x => x.Value.Errors).First();

                if (!string.IsNullOrEmpty(error.ErrorMessage))
                    message = error.ErrorMessage;
                else if (error.Exception?.Message != null)
                    message = error.Exception.Message;
            }

            return message;
        }
    }
}
