using Core.Gateway.Data;
using Core.Gateway.Data.Queries;
using Core.Gateway.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Data.Repository
{
    public class GenericRepository : BaseRepository, IGenericRepository
    {

        private readonly IConfiguration _config;
        private string Connectionstring = "DefaultConnection";
        IDbConnection _db;

        public GenericRepository(IConfiguration configuration) : base(configuration)
        {
            _config = configuration;
            _db = new SqlConnection(_config.GetConnectionString(Connectionstring));

        }

        //public async ValueTask<MerchantInfoResult> MerchantInfo(long processorId)       
        //{
        //    return await WithConnection(async conn =>
        //    {
        //        var param = new DynamicParameters();
        //        param.Add("@processorId", processorId);
        //        return conn.Query<MerchantInfoResult>(_commandText.GetMerchantInfo, param, null, true, 0, System.Data.CommandType.StoredProcedure).SingleOrDefault();
        //    });             
        //}

        public async Task<T> RunQuery<T>(string query, DynamicParameters parms)
        {
            return (await _db.QueryAsync<T>(query, parms, commandType: CommandType.StoredProcedure)).FirstOrDefault();
        }

        public async Task<List<T>> RunQueryList<T>(string query, DynamicParameters parms)
        {
            return (await _db.QueryAsync<T>(query, parms, commandType: CommandType.StoredProcedure)).ToList();
        }
    }
}
