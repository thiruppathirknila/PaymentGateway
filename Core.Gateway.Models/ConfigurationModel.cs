using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Gateway.Models
{
     public class ConfigurationModel
    {
        public string ProcessName { get; set; }
        public string NaeUser { get; set; }
        public string NaePswd { get; set; }
        public string DBUser { get; set; }
        public string DBPswd { get; set; }
        public string Company1 { get; set; }
        public string Company2 { get; set; }
        public string Company3 { get; set; }
        public string Company4 { get; set; }
        public string OperationTypeCC { get; set; }
        public string OperationTypeToken { get; set; }
        public string ServiceUrl { get; set; }
        public int ServicePort { get; set; }
    }
}
