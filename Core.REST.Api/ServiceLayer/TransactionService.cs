using Core.REST.Api.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.REST.Api.ServiceLayer
{
    public class TransactionService : IDisposable
    {
        private readonly IConfiguration _config;

        public TransactionService(IConfiguration config)
        {
            _config = config;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public async Task<dynamic> ProcessRequest(TransactionRequest request)
        {
            var BaseUri = _config.GetSection("BaseUrl").Value;
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                return await client.PostAsync(BaseUri, content);
            }
        }
    }
}
