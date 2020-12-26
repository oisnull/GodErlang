using System;
using System.Collections.Generic;

namespace GodErlang.Entity.Models
{
    public partial class GeneralProduct
    {
        public int Id { get; set; }
        public ProductSourceType SourceType { get; set; }
        public string SourceTypeName { get; set; }
        public string ReferUrl { get; set; }
        public string OriginId { get; set; }
        public string Title { get; set; }
        public string OfferType { get; set; }
        public decimal ActualPrice { get; set; }
        public string ActualPriceDesc { get; set; }
        public decimal PromotionPrice { get; set; }
        public string PromotionPriceDesc { get; set; }
        public string PriceCurrency { get; set; }
        public string ProductImages { get; set; }
        public DateTime RecordTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
