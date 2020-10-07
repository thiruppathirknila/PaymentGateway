using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Gateway.Domain.Interfaces
{
   public interface IMessageService
    {
        Task<string> GetErrorMessage(string errorKey);
    }
}
