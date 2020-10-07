
using Core.Gateway.Domain.Interfaces;
using Core.Gateway.Models;
using System;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Payment.Data.Repository;
using Core.Gateway.Data.Queries;

namespace Core.Gateway.Domain.Services
{
    public class ProcessHelper : IProcessHelper
    {
        private readonly IConfiguration _configuration;
        private readonly IGenericRepository _repository;
        private readonly ICommandText _query;
        public ProcessHelper(IConfiguration configuration, IGenericRepository repository, ICommandText query)
        {
            _configuration = configuration;
            _repository = repository;
            _query = query;
        }
        public async Task<bool> OrderIdIsUnique(MerchantInfoResult merchantInfoResult, String orderId)
        {
            try
            {
                var dbPara = new DynamicParameters();
                dbPara.Add("newOrderId", Convert.ToInt64(orderId));
                dbPara.Add("storename", merchantInfoResult.StoreName);
                dbPara.Add("mid", !string.IsNullOrWhiteSpace(merchantInfoResult.Mid) ? merchantInfoResult.Mid : merchantInfoResult.MerchantNumber);
                dbPara.Add("tid", merchantInfoResult.Tid);
                var result = (await Task.FromResult(_repository.RunQuery<dynamic>(_query.GetOrderIdFoundCount, dbPara)).Result);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           

        }
    }
}
