using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Model.Models
{
    public class CryptogramLookupResultData : GatewayStatusResult
    {
        /*  ------------------------------------------------------------------------
        * 
        *               Successful lookup return data
        * 
        * -------------------------------------------------------------------------*/
        public string CardNumber { get; set; }
        
        public string CardholderName { get; set; }
       
        public string NameOnCard { get; set; }
        
        public string MaskedPan { get; set; }
        
        public string Last4 { get; set; }
        
        public string CardSecurityCode { get; set; }
       
        public string CardExpMonth { get; set; }
        
        public string CardExpYear { get; set; }

        /*  ------------------------------------------------------------------------
        * 
        *              ACH lookup return data
        * 
        * -------------------------------------------------------------------------*/

        public string Aba { get; set; }
       
        public string Dda { get; set; }
        
        public string AccountType { get; set; }
       
        public string BankNumber { get; set; }
        
        public string TransitNumber { get; set; }

        /*  ------------------------------------------------------------------------
        * 
        *              other lookup return data
        * 
        * -------------------------------------------------------------------------*/

        public string FeeAmount; //convenience Fee Amount

        
        public string TranscenterId { get; set; }
        
        public string ProcessorId { get; set; }
        
        public string IpAddress { get; set; }
        /*  ------------------------------------------------------------------------
        * 
        *              Failed lookup return data
        * 
        * -------------------------------------------------------------------------*/
       
        public string Message { get; set; } // all bad requests get a message
        
        public Dictionary<string, string[]> ModelState { get; set; }  // if this was returned something was way off in the request
    }
}
