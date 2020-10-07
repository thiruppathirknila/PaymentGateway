using FAPS.Core.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FAPS.Core.Helper
{
    public partial class ClientApiService
    {
        private readonly Uri _baseUri;

        public ClientApiService(Uri baseUri)
        {
            _baseUri = baseUri;
        }

        #region API Call Methods
        public async Task<string> PostAsyncJsonData<TRequest>(string uri, TransactionRequest requestContent)
        {
            using (var client = ConstructHttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(requestContent), Encoding.UTF8, "application/json");
               // var content = new ObjectContent<TRequest>(requestContent, new System.Net.Http.Formatting.JsonMediaTypeFormatter());
                var response = await client.PostAsync(uri, content);
                var responseContent = await response.Content.ReadAsStringAsync();
                //response.EnsureSuccessStatusCode();
                return responseContent;
            }
        }
        #endregion

        #region private Functions
        private HttpClient ConstructHttpClient()
        {
            return Extensions.ConstructDisposable(() => new HttpClient { BaseAddress = _baseUri },
                client =>
                {
                    client.Timeout = TimeSpan.FromMinutes(3);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                });
        }
        #endregion
    }
}
