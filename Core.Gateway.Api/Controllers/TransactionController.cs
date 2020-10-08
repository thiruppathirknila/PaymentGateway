using System;
using System.Threading.Tasks;
using Core.Gateway.Helper;
using Core.Gateway.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Core.Gateway.Domain.Interfaces;
using Core.Gateway.Interfaces;
using Core.Gateway.Services;

namespace Payment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        #region TransactionController
        private readonly IConfiguration _config;
        private readonly IPaymentService _paymentService;
        private readonly IMessageService _messageService;
        public TransactionController(IConfiguration config,IPaymentService paymentService, IMessageService messageService)
        {
            _config = config;         
            _paymentService = paymentService;
            _messageService = messageService;
        }
        #endregion

        #region Post
        public async Task<IActionResult> Post([FromBody]Request request)
        {
            try
            {
                Logger.InformationLog($"In TransactionController.Post, Post Process Start, Transaction Request: " + JsonConvert.SerializeObject(request));

                using (TransactionService service = new TransactionService(_config, _paymentService, _messageService))
                {
                    var result = await service.ProcessRequest(request);
                    Logger.InformationLog($"In TransactionController.Post, Transaction Response: " + JsonConvert.SerializeObject(result));
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog($"Exception TransactionController.Post .exception={ex.Message}, Trace={ex.StackTrace}");
            }
            finally
            {
                Logger.InformationLog($"Out TransactionController.Post, Post Process End");
            }
            return BadRequest();
        }


        #endregion
    }
}