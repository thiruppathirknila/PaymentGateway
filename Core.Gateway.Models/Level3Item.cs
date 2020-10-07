

namespace Core.Gateway.Models
{
    public   class Level3Item
    {
        public string Description { get; set; }
        public string Number { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal? ItemDiscountAmount { get; set; }
        public decimal? ItemDiscountRate { get; set; }
    }
}
