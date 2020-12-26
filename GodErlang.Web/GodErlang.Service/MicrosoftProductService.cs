//using GodErlang.Entity.Extends;
//using GodErlang.Entity.Models;
//using System;
//using System.Linq;
//using System.Collections.Generic;

//namespace GodErlang.Service
//{
//    public class MicrosoftProductService : BaseService
//    {
//        const string SOURCE_TYPE = "microsoft";
//        const string SOURCE_TYPE_NAME = "microsoft";

//        public void Add(MicrosoftProductModel model)
//        {
//            GeneralProduct product = new GeneralProduct()
//            {
//                OriginId = model.OriginId,
//                ReferUrl = model.ReferUrl,
//                Title = model.Title,
//                ActualPrice = model.ActualPrice,
//                OfferType = model.OfferType,
//                ProductImages = model.ProductImages,

//                SourceType = SOURCE_TYPE,
//                SourceTypeName = SOURCE_TYPE_NAME,
//                RecordTime = DateTime.Now,

//                //Id = 0,
//                //PromotionPrice = null,
//            };
//            db.GeneralProduct.Add(product);
//            db.SaveChanges();
//        }

//        public List<MicrosoftProductModel> GetAll()
//        {
//            return db.GeneralProduct
//                .Where(c => c.SourceType == SOURCE_TYPE)
//                .Select(c => new MicrosoftProductModel
//                {
//                    ReferUrl = c.ReferUrl,
//                    OriginId = c.OriginId,
//                    Title = c.Title,
//                    ActualPrice = c.ActualPrice,
//                    OfferType = c.OfferType,
//                    ProductImages = c.ProductImages,
//                })
//                .ToList();
//        }
//    }
//}
