using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Gateway.Data.Queries
{
    public interface ICommandText
    {
        string GetMerchantInfo { get; }
        string GetUpdatedCardDetails { get; }
        string MerchantConfigWithMidTid { get; }
        string MerchantConfigWithAccountType { get; }
        string MerchantConfigAccTypeMoto { get; }
        string MerchantConfigAccTypeRetail { get; }
        string GetOrderIdFoundCount { get; }
    }
}
