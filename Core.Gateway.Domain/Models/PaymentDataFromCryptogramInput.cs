using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Model.Models
{
    public class PaymentDataFromCryptogramInput
    {
        public string Cryptogram { get; set; }
        public string TransactionType { get; set; }
    }
}
