using Core.REST.Api.Models;
using Core.REST.Api.ServiceLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Core.REST.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IConfiguration _config;

        public TransactionsController(IConfiguration config)
        {
            _config = config;
        }
        [HttpGet]
        public async Task<IActionResult>Get()
        {
            return Ok("Application running...");
        }
        [HttpPost]
        public async Task<IActionResult> Post(TransactionRequest request)
        {
            try
            {
                using (var service = new TransactionService(_config))
                {
                    var result = await service.ProcessRequest(request);
                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
