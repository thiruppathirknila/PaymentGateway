using Core.Gateway.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Gateway.Domain.Interfaces
{
    public interface IProcessHelper
    {
        Task<bool> OrderIdIsUnique(MerchantInfoResult merchantInfoResult, String orderId);
    }
}
