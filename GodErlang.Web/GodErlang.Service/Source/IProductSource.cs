using GodErlang.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GodErlang.Service.Source
{
    public interface IProductSource
    {
        ProductSourceType GetSourceType();
        string GetOriginId();
        string GetTitle();
        string GetActualPriceDesc();
        string GetPromotionPriceDesc();
        string GetPriceCurrency();
        string GetOfferType();
        string ProductImages();
    }
}
