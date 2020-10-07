using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Gateway.Interfaces
{
    public interface IOrder
    {
        string orderId { get; set; }
        bool? orderIdIsUnique { get; set; }
        bool? autoGenerateorderId { get; set; }
    }
}
