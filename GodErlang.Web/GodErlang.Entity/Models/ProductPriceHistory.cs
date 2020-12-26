using System;
using System.Collections.Generic;

namespace GodErlang.Entity.Models
{
    public partial class ProductPriceHistory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal ActualPrice { get; set; }
        public decimal PromotionPrice { get; set; }
        public string PriceCurrency { get; set; }
        public DateTime RecordTime { get; set; }
    }
}
