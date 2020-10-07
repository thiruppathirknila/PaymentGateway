using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Gateway.Models
{
    public class TRequest
    {
        
        public string StoreName{ get; set; }
        public string OrderId { get; set; }
        public string IpAddress { get; set; }
        public string AuthResponse { get; set; }
        public string AuthCode { get; set; }
        public string AvsResponse { get; set; }
        public string Type { get; set; }
        public string EtcTransType { get; set; }
        public string Cvv2Response { get; set; }
        public string ReferenceNumber { get; set; }
        public string Processor { get; set; }
        public string RecurringType { get; set; }
        public string Script { get; set; }
        public string ErrorMessage { get; set; }
        public string TransType { get; set; }
        public string PostedBy { get; set; }
        public string PaymentCryptogram { get; set; }
        public string EciIndicator { get; set; }
        public string Stin { get; set; }
        public string SalesTax { get; set; }
        public bool Success { get; set; }
        public bool Recurring { get; set; }
        public bool FraudCheck { get; set; }
        public bool Debug { get; set; }
        public bool VoiceAuth { get; set; }
        public bool Scheduled { get; set; }
        public bool IsAstrex { get; set; }
        public int MidTidId { get; set; }
        public double Amount { get; set; }
        public double stax { get; set; }
        public CreditCard CCard { get; set; }
        public ContactInfo CheckInfo { get; set; }

    }
}
