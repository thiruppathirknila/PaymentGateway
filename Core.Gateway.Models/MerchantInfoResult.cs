using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Gateway.Models
{
    public partial class MerchantInfoResult
    {
        public string MerchantNumber { get; set; }
        public string Mid { get; set; }
        public string Tid { get; set; }
        public int MerchantId { get; set; }
        public string StoreName { get; set; }
        public Nullable<System.Guid> GatewayId { get; set; }
        public string AccountType { get; set; }
        public Nullable<bool> Disabled { get; set; }
        public bool HasMultiMidTid { get; set; }
        public string Product { get; set; }
        public bool Ach_on { get; set; }
        public bool Cc_on { get; set; }
        public string Firstfund_username { get; set; }
        public bool Cim_on { get; set; }
        public string Processor { get; set; }
        public Nullable<bool> Batch_on_demand_allowed { get; set; }
        public int FeeMidTid { get; set; }
        public string IpAddress { get; set; }
    }
}
