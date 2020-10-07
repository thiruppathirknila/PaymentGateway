using FAPS.Core.Helper;
using FAPS.Core.Helper.ApiService;
using FAPS.Core.RestApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FAPS.Core.Model;

namespace FAPS.Core.RestApp.Controllers
{
    public class RestApiController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public RestApiController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Home(string requestType)
        {
            RestApiViewModel model = new RestApiViewModel();
            model.RequestType = requestType;
            var filePath = _hostingEnvironment.WebRootPath + @"\json\ApiRequest.json";

            model.TransationRequest = requestType.GetJosnStringFromJsonFile(filePath);
            model.TransationResponse = string.Empty;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Home(RestApiViewModel model)
        {
            var client = new RestApiService(new Uri(ConstantString.RestApiBaseURI)); 
            var response = await client.GetRestApiResponse(JsonConvert.DeserializeObject<TransactionRequest>(model.TransationRequest));
            model.TransationResponse = response;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaymentBaseJson(string paymentType, string requestType)
        {
            var RequestPath = _hostingEnvironment.WebRootPath + @"\json\ApiRequest.json";
            var filePaymentBaseModelPath = _hostingEnvironment.WebRootPath + @"\json\PaymentType.json";
            var paymentTypeJson = JObject.Parse(paymentType.GetPaymentTypeJson(filePaymentBaseModelPath));
            var requestJson = requestType.GetJosnStringFromJsonFile(RequestPath);

            if (!paymentTypeJson.IsNull())
            {
                JObject jObject = JObject.Parse(requestJson);
                JToken jPaymentToken = jObject;
                //Set payment type
                jPaymentToken["PaymentMethod"] = paymentTypeJson;
                requestJson = jPaymentToken.ToString();
            }

            return Json(requestJson);
        }
    }


}
