using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Model.Models
{
    public class GatewayStatusResult
    {      
        public bool isError { get; set; }       
        public IList<string> errorMessages { get; set; }        
        public bool validationHasFailed { get; set; }        
        public IList<ValidationFailedMsg> validationFailures { get; set; }        
        public bool isSuccess { get; set; }        
        public string action { get; set; }
    }
}
