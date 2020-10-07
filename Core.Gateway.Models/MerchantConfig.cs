
namespace Core.Gateway.Models
{
    public class MerchantConfig
    {
        public string StoreName { get; set; }
        public string MidTidId { get; set; }
        public bool ContactlessMSDEnabled { get; set; }
        public string ActualAccountType { get; set; }
        public string MerchantPassword { get; set; }
        public string MerchantNumber { get; set; }
        public string TerminalNumber { get; set; }
        public string Email { get; set; }
        public bool CountDs { get; set; }
        public string HighestTicket { get; set; }
        public string MaxVol { get; set; }
        public string MerchantNumberRetail { get; set; }
        public string TerminalNumberRetail { get; set; }
        public string TerminalNumberMoto { get; set; }
        public string MerchantDba { get; set; }
        public string ClientNumber { get; set; }
        public bool IsFaps { get; set; }
        public string NcUser { get; set; }
        public string NcPwd { get; set; }
        public bool IsRestaurant { get; set; }
        public bool IsLodging{ get; set; }
        public string AmexCardAcceptorBusinessCode { get; set; }

    }
}
