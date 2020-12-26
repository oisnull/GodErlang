using GodErlang.Common;
using GodErlang.Entity;
using GodErlang.Entity.Models;
using GodErlang.Service.Source;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GodErlang.Service
{
    public class ProductService : BaseService
    {
        public ProductService(GodErlangEntities db) : base(db)
        {
        }

        #region Product details manage

        public ProductDetails GetDetailsByOriginId(ProductSourceType sourceType, string originId)
        {
            if (string.IsNullOrEmpty(originId))
                return null;

            return db.ProductDetails.FirstOrDefault(g => g.SourceType == sourceType && g.OriginId == originId);
        }

        /// <summary>
        /// If the product already exists by OriginId and will not be insert into db
        /// </summary>
        public int SaveDetails(string productUrl, IProductSource productSource)
        {
            if (string.IsNullOrEmpty(productUrl) || productSource == null)
                return 0;

            string originId = productSource.GetOriginId();
            ProductSourceType sourceType = productSource.GetSourceType();
            ProductDetails old = GetDetailsByOriginId(sourceType, originId);
            if (old != null) return 0;

            return AddDetails(productUrl, productSource)?.Id ?? 0;
        }

        public ProductDetails AddDetails(string productUrl, IProductSource productSource)
        {
            if (string.IsNullOrEmpty(productUrl) || productSource == null)
                return null;

            ProductDetails product = new ProductDetails()
            {
                //Id = 0,
                ReferUrl = productUrl,
                OriginId = productSource.GetOriginId(),
                Title = productSource.GetTitle(),
                ActualPriceDesc = productSource.GetActualPriceDesc(),
                OfferType = productSource.GetOfferType(),
                ProductImages = productSource.ProductImages(),
                PromotionPriceDesc = productSource.GetPromotionPriceDesc(),
                PriceCurrency = productSource.GetPriceCurrency(),
                SourceType = productSource.GetSourceType(),
                SourceTypeName = CustomEnum.GetSourceTypeName(productSource.GetSourceType()),
                RecordTime = DateTime.Now,
            };
            product.ActualPrice = CommonTools.ExtractFirstPrice(product.ActualPriceDesc);
            product.PromotionPrice = CommonTools.ExtractFirstPrice(product.PromotionPriceDesc);

            db.ProductDetails.Add(product);
            db.SaveChanges();
            return product;
        }

        #endregion

        #region Product status manage

        public List<ProductStatus> GetLastMonthStatus(int userId, ProductExecState? state = null)
        {
            DateTime lastMonth = DateTime.Now.AddDays(-30);
            IQueryable<ProductStatus> query = db.ProductStatus.Where(p => p.UserId == userId && p.AddTime >= lastMonth);
            if (state != null)
            {
                query = query.Where(p => p.State == state.Value);
            }

            return query.ToList();
        }

        public void AddStatus(int userId, string productUrl, bool execTrigger = true)
        {
            if (userId <= 0) return;

            if (string.IsNullOrEmpty(productUrl))
                throw new Exception("The product url can't be empty.");

            if (db.ProductStatus.Count(p => p.UserId == userId && p.ReferUrl == productUrl) > 0)
                throw new Exception("A similar product already exists");

            ProductStatus status = new ProductStatus()
            {
                UserId = userId,
                ReferUrl = productUrl,
                State = ProductExecState.NotStart,
                AddTime = DateTime.Now,
            };
            db.ProductStatus.Add(status);
            int count = db.SaveChanges();

            if (count > 0 && execTrigger)
            {
                Storage.ProductManager.AddUrlAndTrigger(userId, status.Id, productUrl);
            }
        }

        public void DeleteStatus(int userId, int statusId)
        {
            if (userId <= 0) return;
            if (statusId <= 0)
                throw new Exception("The product status id can't be empty.");

            var status = db.ProductStatus.Where(p => p.UserId == userId && p.Id == statusId).FirstOrDefault();
            if (status == null)
                throw new Exception($"Not found product status by statusId={statusId}");

            db.ProductStatus.Remove(status);
            int count = db.SaveChanges();
            if (count > 0 && status.State == ProductExecState.NotStart)
            {
                Storage.ProductManager.RemoveUrl(userId, status.Id, status.ReferUrl);
            }
        }

        public void SetStart(int productStatusId)
        {
            this.SetStatus(productStatusId, 0, null, ProductExecState.Running);
        }

        public void SetEnd(int productStatusId)
        {
            this.SetStatus(productStatusId, 0, null, ProductExecState.Completed);
        }

        public void SetEndFailed(int productStatusId, string execMessage)
        {
            this.SetStatus(productStatusId, 0, null, ProductExecState.RunFailed, execMessage);
        }

        public void SetStart(int userId, string productUrl)
        {
            this.SetStatus(0, userId, productUrl, ProductExecState.Running);
        }

        public void SetEnd(int userId, string productUrl)
        {
            this.SetStatus(0, userId, productUrl, ProductExecState.Completed);
        }

        private void SetStatus(int productStatusId, int userId, string productUrl, ProductExecState state, string execMessage = null)
        {
            ProductStatus status = null;
            if (productStatusId > 0)
            {
                status = db.ProductStatus.Where(p => p.Id == productStatusId).FirstOrDefault();
            }
            else if (userId > 0 && !string.IsNullOrEmpty(productUrl?.Trim()))
            {
                status = db.ProductStatus.Where(p => p.UserId == userId && p.ReferUrl == productUrl).FirstOrDefault();
            }

            if (status == null) return;

            status.State = state;
            status.ExecMessage = execMessage;
            switch (status.State)
            {
                case ProductExecState.Running:
                    status.StartExecTime = DateTime.Now;
                    break;
                case ProductExecState.RunFailed:
                case ProductExecState.Completed:
                    status.EndExecTime = DateTime.Now;
                    break;
                case ProductExecState.NotStart:
                default:
                    break;
            }
            db.SaveChanges();
        }
        #endregion

        public void Update(ProductDetails product)
        {
            if (product == null || product.Id <= 0) return;

            product.UpdateTime = DateTime.Now;
            db.SaveChanges();
        }

        public List<ProductDetails> GetAll()
        {
            return db.ProductDetails.ToList();
        }

        public ProductDetails Get(ProductSourceType sourceType, string originId)
        {
            if (string.IsNullOrEmpty(originId))
                return null;

            return db.ProductDetails.FirstOrDefault(g => g.SourceType == sourceType && g.OriginId == originId);
        }

        #region Product price history manage

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
