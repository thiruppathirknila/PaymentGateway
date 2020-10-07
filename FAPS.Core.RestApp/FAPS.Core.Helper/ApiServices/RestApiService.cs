using FAPS.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FAPS.Core.Helper.ApiService
{
    public class RestApiService
    {
        private readonly Uri _baseUri;
        private readonly ClientApiService clientService;


        public RestApiService(Uri baseUri)
        {
            _baseUri = baseUri;
            clientService = new ClientApiService(_baseUri);

        }

        #region Form Builder API services  

        public async Task<string> GetRestApiResponse(TransactionRequest transactionRequest)
        {
            try
            {
                return await clientService.PostAsyncJsonData<TransactionRequest>($"{_baseUri}api/Transactions", transactionRequest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
