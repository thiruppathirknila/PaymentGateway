using Core.Gateway.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Gateway.Helper
{
   public class CryptogramHelper
    {

        /// <summary>
        /// Given a cryptogram and, optionally, merchant lookup information, send a request to Cryptogram API to tell it we no longer want this cryptogram to be valid.
        /// </summary>
        /// <param name="cryptogram"></param>
        /// <param name="request"></param>
        /// <returns>boolean to indicate if we got success</returns>
        public static bool ForceExpireCryptogramAsync(string getAPIName,string cryptogram, Request request)
        {
            bool expiredSuccessfully = false;
            string requestString = string.Empty;
            string responseData = string.Empty;
            StringBuilder tracing = new StringBuilder();
            try
            {
                string url = $"{getAPIName}Expire";
                if (string.IsNullOrWhiteSpace(cryptogram)) return expiredSuccessfully;     // No cryptogram = not going to expire anything
                try
                {
                    using (var wb = new WebClient())
                    {
                        wb.Headers.Add("Content-Type", "application/json");
                        var expireData = new Dictionary<string, string>
                        {
                            { "cryptogram", cryptogram }
                        };
                        // Merchant information is not strictly necessary. It can be provided so that we have better tracking of what got sent by whom.
                        if (request.MerchantInfo != null)
                        {
                            //expireData["transcenterId"] = request.MerchantInfo.merchantId.ToString();
                            expireData["processorId"] = request.MerchantInfo.ProcessorId.ToString();
                        }
                        requestString = JsonConvert.SerializeObject(expireData);
                        tracing.AppendLine($"REQUEST: {requestString}");
                        var requestBytes = Encoding.UTF8.GetBytes(requestString);
                        var response = wb.UploadData(url, "POST", requestBytes);
                        responseData = Encoding.UTF8.GetString(response);
                        tracing.AppendLine($"RESPONSE: {responseData}");
                        expiredSuccessfully = true;
                    }
                }
                catch (WebException wex)
                {
                    if (wex.Response != null)
                    {
                        using (WebResponse response = wex.Response)
                        {
                            HttpWebResponse httpResponse = (HttpWebResponse)response;
                            using (Stream data = response.GetResponseStream())
                            using (var reader = new StreamReader(data))
                            {
                                responseData = reader.ReadToEnd();
                            }
                        }
                    }
                    tracing.AppendLine($"RESPONSE: {responseData}");
                    expiredSuccessfully = false;
                }
            }
            catch (Exception e)
            {
               // LoggingHelper.LogAnErrorMessage(e, tracing, (info ?? new MerchantInfo()), info?.ipAddress, "CryptogramHelper::ForceExpireCryptogram");
            }

            // Want to be sure that I am getting the expected data in DEBUG builds
          //  MailExtensions.SendMailDebug(tracing.ToString(), string.Empty, "ForceExpireCryptogram Result", string.Empty, "CryptogramHelper::ForceExpireCryptogram");

            return expiredSuccessfully;
        }
    }
}
