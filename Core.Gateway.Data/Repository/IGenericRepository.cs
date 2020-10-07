using Core.Gateway.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Data.Repository
{
    public interface IGenericRepository
    {     
        //ValueTask<MerchantInfoResult> MerchantInfo(long ProcessorId);

        Task <T>RunQuery<T>(string query, DynamicParameters parms);
        Task <List<T>> RunQueryList<T>(string query, DynamicParameters parms);

    }
}
