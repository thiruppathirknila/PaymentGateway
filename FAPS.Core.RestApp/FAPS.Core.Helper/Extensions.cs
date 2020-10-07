using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace FAPS.Core.Helper
{
    public static class Extensions
    {
        public static T Clone<T>(this T t)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(t));
        }

        public static T ConstructDisposable<T>(Action<T> initializer) where T : class, IDisposable, new()
        {
            return ConstructDisposable(() => new T(), initializer);
        }

        public static T ConstructDisposable<T>(Func<T> constructor, Action<T> initializer) where T : class, IDisposable
        {
            if (constructor == null) throw new ArgumentNullException(nameof(constructor));
            if (initializer == null) throw new ArgumentNullException(nameof(initializer));

            var value = constructor();
            T result;

            try
            {
                initializer(value);
                result = value;
                value = null;
            }
            finally
            {
                if (value != null)
                {
                    value.Dispose();
                    result = null;
                }
            }

            return result;
        }

        public static string AddQueryString(object obj)
        {
            var pairs = string.Join("&", QueryStringPairs(obj));
            return string.IsNullOrWhiteSpace(pairs) ? string.Empty : $"?{pairs}";
        }

        public static IEnumerable<string> QueryStringPairs(object obj)
        {
            return
                from prty in TypeDescriptor.GetProperties(obj).Cast<PropertyDescriptor>()
                let val = prty.GetValue(obj)
                where (val != null)
                let strVal = (prty.Converter?.CanConvertTo(typeof(string)) ?? false)
                ? prty.Converter.ConvertToString(val) : val.ToString()
                select $"{prty.Name}={Uri.EscapeDataString(strVal ?? string.Empty)}";
        }

        #region Serialize class objects to JSON
        public static string ToJSON(this object obj)
        {
            string jsonString = string.Empty;

            if (obj.IsNull()) return string.Empty;

            jsonString = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None,
                                                                new JsonSerializerSettings
                                                                {
                                                                    DefaultValueHandling = DefaultValueHandling.Ignore
                                                                });
            if (jsonString.IsNullOrEmpty()) return string.Empty;
            return jsonString;
        }
        #endregion

        #region Deserialize JSON string to class objects
        public static T FromJSON<T>(this string jsonString)
        {
            if (jsonString.IsNullOrEmpty()) return default(T);
            try
            {
                return JsonConvert.DeserializeObject<T>(jsonString, new JsonSerializerSettings
                {
                    DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore,
                    Culture = System.Globalization.CultureInfo.CurrentCulture
                });
            }
            catch (Exception)
            {

            }
            return default(T);
        }
        #endregion

        #region Serialize JSON string to JSON oject
        public static JObject ToJsonOject(string jsonString)
        {
            if (jsonString.IsNullOrEmpty()) return null;
            JObject jObject = JObject.Parse(jsonString);
            return jObject;
        }
        #endregion

        #region check if object is null
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }
        #endregion

        #region check if string is null or empty
        public static bool IsNullOrEmpty(this string str)
        {
            return str == null || str.Trim().Length == 0;
        }
        #endregion

        public static string GetJosnStringFromJsonFile(this string requestType, string rootPath)
        {
            var apiRequest = string.Empty;

            var Json = System.IO.File.ReadAllText(rootPath);

            if (!Json.IsNullOrEmpty())
            {
                JObject jObject = JObject.Parse(Json);
                JToken jTransactionRequest = jObject;

                switch (requestType)
                {
                    case ERequestType.SALE:
                    case ERequestType.AUTHORIZE:
                        //set RequestType
                        jTransactionRequest["AuthSale"]["RequestType"] = requestType;
                        apiRequest = jTransactionRequest["AuthSale"].ToString();
                        break;
                    case ERequestType.CREDIT:
                    case ERequestType.DEBIT:
                        jTransactionRequest["ACH"]["RequestType"] = requestType;
                        apiRequest = jTransactionRequest["ACH"].ToString();
                        break;
                    case ERequestType.REFUND:
                        apiRequest = jTransactionRequest["Refund"].ToString();
                        break;
                   
                    case ERequestType.SETTLE:
                        apiRequest = jTransactionRequest["Settle"].ToString();
                        break;
                    case ERequestType.TIPADJUST:
                        apiRequest = jTransactionRequest["TipAdjust"].ToString();
                        break;
                    case ERequestType.VOID:
                        apiRequest = jTransactionRequest["Void"].ToString();
                        break;
                    default:
                        break;
                }
            }
            return apiRequest;
        }


        public static string GetPaymentTypeJson(this string paymentType, string rootPath)
        {
            var paymentRequestJson = string.Empty;

            var Json = System.IO.File.ReadAllText(rootPath);

            if (!Json.IsNullOrEmpty())
            {
                JObject jObject = JObject.Parse(Json);
                JToken jToken = jObject;

                switch (paymentType)
                {
                    case "CreditCard":
                        paymentRequestJson = jToken["CreditCardInfo"].ToString();
                        break;
                    case "Token":
                        paymentRequestJson = jToken["TokenInfo"].ToString();
                        break;
                    case "MagData":
                        paymentRequestJson = jToken["MagDataInfo"].ToString();
                        break;
                    case "CryptoGram":
                        paymentRequestJson = jToken["CryptoGramInfo"].ToString();
                        break;
                    case "Vault":
                        paymentRequestJson = jToken["VaultInfo"].ToString();
                        break;
                    default:
                        // code block
                        break;
                }
            }
            return paymentRequestJson;
        }


        public static bool IsValidJson(this string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput)) { return false; }
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


    }
}
