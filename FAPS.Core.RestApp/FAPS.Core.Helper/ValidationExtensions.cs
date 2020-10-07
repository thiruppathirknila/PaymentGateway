using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FAPS.Core.Helper
{
    public static class ValidationExtensions
    {
        public static bool IsValidJson(this string jsonString, IHostingEnvironment hostingEnvironment, out IList<string> validationError , string requestType = null , string paymentType = null)
        {
            validationError = new List<string>();
            JObject jObject = null;
            IList<string> templateKeys = new List<string>();
            IList<string> requestKeys = new List<string>(); 

            var requetJson = JObject.Parse(jsonString);

            if (!requestType.IsNullOrEmpty())
            {
                var requestJson = System.IO.File.ReadAllText(hostingEnvironment.WebRootPath + @"\json\ApiRequest.json");

                if (!requestJson.IsNullOrEmpty())
                    jObject = JObject.Parse(requestJson);

                JToken jToken = jObject;

                switch (requestType)
                {
                    case ERequestType.SALE:
                    case ERequestType.AUTHORIZE:
                         templateKeys = JObject.Parse(jToken["AuthSale"].ToString()).Properties().Select(p => p.Name).ToList();
                        break;
                    case ERequestType.CREDIT:
                    case ERequestType.DEBIT:
                        templateKeys = JObject.Parse(jToken["ACH"].ToString()).Properties().Select(p => p.Name).ToList();
                        break;
                    case ERequestType.REFUND:
                        templateKeys = JObject.Parse(jToken["Refund"].ToString()).Properties().Select(p => p.Name).ToList();
                        break;

                    case ERequestType.SETTLE:
                        templateKeys = JObject.Parse(jToken["Settle"].ToString()).Properties().Select(p => p.Name).ToList();
                        break;
                    case ERequestType.TIPADJUST:
                        templateKeys = JObject.Parse(jToken["TipAdjust"].ToString()).Properties().Select(p => p.Name).ToList();
                        break;
                    case ERequestType.VOID:
                        templateKeys = JObject.Parse(jToken["Void"].ToString()).Properties().Select(p => p.Name).ToList();
                        break;
                    default: 
                        break;
                }


                if (templateKeys.Count > 0)
                {
                    requestKeys = JObject.Parse(jsonString).Properties().Select(p => p.Name).ToList();
                    var result = templateKeys.Where(b => !requestKeys.Any(a => b.Contains(a))).ToList();

                    if (result != null)
                    {
                        foreach(var item in result)
                        {
                            validationError.Add($"{item} property missing");
                        }
                        return false;
                    }
                }

            }

            return true;
        }
    }
}
