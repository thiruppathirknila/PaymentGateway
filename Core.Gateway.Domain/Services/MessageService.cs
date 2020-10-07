using Core.Gateway.Domain.Helpers;
using Core.Gateway.Domain.Interfaces;
using Core.Gateway.Models;
using Dapper;
using Payment.Data.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Core.Gateway.Domain.Services
{
   public class MessageService : IMessageService
    {
        private readonly IGenericRepository _repository;
        public MessageService(IGenericRepository repository)
        {
            _repository = repository;
        }
        public async Task<string> GetErrorMessage(string errorKey)
        {
            try
            {
                var dbPara = new DynamicParameters();
                dbPara.Add("MessageCode", errorKey);

                var result = await Task.FromResult(_repository.Get<ErrorMessage>(Query.GetErrorValue, dbPara, commandType: CommandType.Text));
                if (result != null)
                {
                    return result.MessageValue;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
