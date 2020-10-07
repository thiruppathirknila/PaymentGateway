using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Gateway.Models
{
    public class CreditCard
    {
        public string CardType { get; set; }
        public string CardNum { get; set; }
        public string OrigMagData { get; set; }
        public string MagData { get; set; }
        public string MagType { get; set; }
        public string ExpMonth { get; set; }
        public string ExpYear { get; set; }
        public string Cvv2 { get; set; }
        public bool PurchaseCard { get; set; }
    }
}
