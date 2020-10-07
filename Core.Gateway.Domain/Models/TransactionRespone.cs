using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Service.Models
{
   public  class TransactionRespone
    {
        public string AuthResponse { get; set; }
        public string AuthCode { get; set; }
        public string ReferenceNumber { get; set; }               
        public bool IsPartial { get; set; }
        public string PartialId { get; set; }      
        public decimal OriginalFullAmount { get; set; }
        public decimal PartialAmountApproved { get; set; }
        public string AvsResponse { get; set; }     
        public string Cvv2Response { get; set; }
        public string OrderId { get; set; }        
        public string CardType { get; set; }
        public string Last4 { get; set; }
        public string MaskedPan { get; set; }
        public string Token { get; set; }
        public decimal CreditAmount { get; set; }
    }
}