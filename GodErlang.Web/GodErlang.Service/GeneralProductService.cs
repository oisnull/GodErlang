using GodErlang.Entity;
using GodErlang.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GodErlang.Service
{
    public class GeneralProductService : BaseService
    {
        public GeneralProductService(GodErlangEntities db) : base(db)
        {
        }

        public void Save(GeneralProduct product)
        {
            if (product == null) return;

            GeneralProduct old = Get(product.SourceType, product.OriginId);
            if (old == null)
            {
                db.GeneralProduct.Add(product);
            }
            else
            {
                old.Title = product.Title;
                old.OfferType = product.OfferType;
                old.ActualPrice = product.ActualPrice;
                old.ActualPriceDesc = product.ActualPriceDesc;
                old.PromotionPrice = product.PromotionPrice;
                old.PromotionPriceDesc = product.PromotionPriceDesc;
                old.PriceCurrency = product.PriceCurrency;
                old.ProductImages = product.ProductImages;
                old.UpdateTime = DateTime.Now;
            }

            db.SaveChanges();
        }

        public void Add(GeneralProduct product)
        {
            if (product == null) return;

            db.GeneralProduct.Add(product);
            db.SaveChanges();
        }

        public void Update(GeneralProduct product)
        {
            if (product == null || product.Id <= 0) return;

            product.UpdateTime = DateTime.Now;
            db.SaveChanges();
        }

        public List<GeneralProduct> GetAll()
        {
            return db.GeneralProduct.ToList();
        }

        public GeneralProduct Get(ProductSourceType sourceType, string originId)
        {
            if (string.IsNullOrEmpty(originId))
                return null;

            return db.GeneralProduct.FirstOrDefault(g => g.SourceType == sourceType && g.OriginId == originId);
        }

        #region Product price history operate

        public void AddHistory(int productId, decimal actualPrice, decimal promotionPrice, string priceCurrency = null)
        {
            ProductPriceHistory history = new ProductPriceHistory()
            {
                ProductId = productId,
                ActualPrice = actualPrice,
                PromotionPrice = promotionPrice,
                PriceCurrency = priceCurrency,
                RecordTime = DateTime.Now
            };
            db.ProductPriceHistory.Add(history);
            db.SaveChanges();
        }

        #endregion
    }
}
