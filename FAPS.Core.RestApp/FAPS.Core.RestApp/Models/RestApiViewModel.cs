using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FAPS.Core.RestApp.Models
{

    public class RestApiViewModel
    {
        public string TransationRequest { get; set; }
        public string TransationResponse { get; set; }
        public string PaymentType { get; set; }
        public string RequestType { get; set; }
    } 
}
