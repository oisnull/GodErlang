using GodErlang.Common;
using GodErlang.Entity;
using GodErlang.Entity.Models;
using GodErlang.Service;
using GodErlang.Service.Source;
using GodErlang.Service.Storage;
using Newtonsoft.Json;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GodErlang.ConsoleTest
{
    class Program
    {
        static GodErlangEntities DBContext { get; } = GodErlangDBContext.GetDBContext(ShareConfig.AppConfigManager.DBConnectionString);

        static void Save(string referUrl, IProductSource productSource)
        {
            if (productSource == null) return;

            GeneralProduct product = new GeneralProduct()
            {
                //Id = 0,
                ReferUrl = referUrl,
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

            if (string.IsNullOrEmpty(product.OriginId))
            {
                Output("OriginId is empty", ConsoleColor.Red);
            }
            else
            {
                Output(JsonConvert.SerializeObject(product), ConsoleColor.Green);

                GeneralProductService productService = new GeneralProductService(DBContext);
                productService.Save(product);

                Output("Saved", ConsoleColor.Green);
            }
        }

        static void FetchTest()
        {
            using (IWebDriver driver = WebDriverLoader.GetChromeDriver())
            {
                foreach (var item in InitConfig.TEST_URLS)
                {
                    driver.Navigate().GoToUrl(item.SourceUrl);

                    IProductSource productSource = ProductSourceFactory.Builder(driver, item.SourceType);
                    Save(driver.Url, productSource);
                }
            }
        }

        static void ResetDatas()
        {
            GeneralProductService productService = new GeneralProductService(DBContext);
            List<GeneralProduct> products = productService.GetAll();
            using (IWebDriver driver = WebDriverLoader.GetChromeDriver())
            {
                foreach (var item in products)
                {
                    driver.Navigate().GoToUrl(item.ReferUrl);

                    IProductSource productSource = ProductSourceFactory.Builder(driver, item.SourceType);
                    if (productSource == null)
                    {
                        Output($"{item.Id}/{item.OriginId}: {item.SourceType} is invalid", ConsoleColor.Red);
                    }
                    else
                    {
                        Save(driver.Url, productSource);
                    }
                }
            }
        }

        static void MonitoringTest()
        {
            GeneralProductService productService = new GeneralProductService(DBContext);
            List<GeneralProduct> products = productService.GetAll();

            using (IWebDriver driver = WebDriverLoader.GetChromeDriver())
            {
                foreach (var item in products)
                {
                    driver.Navigate().GoToUrl(item.ReferUrl);

                    IProductSource productSource = ProductSourceFactory.Builder(driver, item.SourceType);
                    if (productSource == null)
                    {
                        Output($"{item.Id}/{item.OriginId}: {item.SourceType} is invalid", ConsoleColor.Red);
                    }
                    else
                    {
                        decimal newActualPrice = CommonTools.ExtractFirstPrice(productSource.GetActualPriceDesc());
                        decimal newPromotionPrice = CommonTools.ExtractFirstPrice(productSource.GetPromotionPriceDesc());

                        decimal currentActualPrice = item.ActualPrice - newActualPrice;
                        decimal currentPromotionPrice = item.PromotionPrice - newPromotionPrice;
                        bool priceChanged = false;
                        if (currentActualPrice > 0)
                        {
                            Output($"The system detected that the price of {item.SourceTypeName} products decreased from {item.ActualPrice} to {newActualPrice}, a decrease of {currentActualPrice}.", ConsoleColor.Yellow);
                            priceChanged = true;
                        }
                        else if (currentActualPrice < 0)
                        {
                            priceChanged = true;
                        }

                        if (currentPromotionPrice > 0)
                        {
                            Output($"The system detected that the promotion price of {item.SourceTypeName} products decreased from {item.ActualPrice} to {newActualPrice}, a decrease of {currentActualPrice}.", ConsoleColor.Yellow);
                            priceChanged = true;
                        }
                        else if (currentPromotionPrice < 0)
                        {
                            priceChanged = true;
                        }

                        if (priceChanged)
                        {
                            productService.AddHistory(item.Id, item.ActualPrice, item.PromotionPrice, item.PriceCurrency);

                            item.ActualPrice = newActualPrice;
                            item.PromotionPrice = newPromotionPrice;
                            productService.Update(item);
                        }
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            //FetchTest();
            //ResetDatas();
            //MonitoringTest();

            //Console.WriteLine("THE END");
            //Console.ReadKey();

            //RdsSubTest();

            UrlMonitoring();
        }

        static void UrlMonitoring()
        {
            ExecuteTask.DefaultInstance.ExtractProcess = value =>
            {
                return Task.Run(() =>
                {
                    Console.WriteLine("Sleep 30s......");
                    Thread.Sleep(30 * 1000);
                    Tuple<int, int, string> queueValue = ProductManager.GetUrlsQueueValue(value);
                    ProductService productService = new ProductService(DBContext);
                    productService.SetStart(queueValue.Item2);
                    using (IWebDriver driver = WebDriverLoader.GetChromeDriver())
                    {
                        driver.Navigate().GoToUrl(queueValue.Item3);

                        IProductSource productSource = ProductSourceFactory.Builder(driver, CustomEnum.GetSourceTypeByUrl(queueValue.Item3));
                        string originId = productSource.GetOriginId();
                        ProductSourceType sourceType = productSource.GetSourceType();
                        string sourceTypeName = CustomEnum.GetSourceTypeName(sourceType);
                        if (string.IsNullOrEmpty(originId))
                        {
                            Output($"The OriginId extracted from url of {value} is empty", ConsoleColor.Red);
                            productService.SetEndFailed(queueValue.Item2, $"{sourceTypeName} OriginId is empty");
                            return;
                        }

                        ProductDetails oldProduct = productService.GetDetailsByOriginId(sourceType, originId);
                        if (oldProduct != null)
                        {
                            Output($"The OriginId of {originId} already exists in db, related info:{value}", ConsoleColor.Red);
                            productService.SetEndFailed(queueValue.Item2, $"{sourceTypeName} OriginId({originId}) exists in db");
                            return;
                        }

                        ProductDetails product = productService.AddDetails(queueValue.Item3, productSource);
                        if (product == null)
                        {
                            Output($"Add product details into db failed, related info:{value}", ConsoleColor.Red);
                            productService.SetEndFailed(queueValue.Item2, $"Add {sourceTypeName} OriginId into db failed");
                            return;
                        }

                        productService.SetEnd(queueValue.Item2);
                        Output(JsonConvert.SerializeObject(product), ConsoleColor.Green);
                    }
                });
            };
            ExecuteTask.DefaultInstance.StartMonitoring();
            Console.WriteLine("Monitoring start ...");
            Console.ReadKey();
        }

        //static void RdsSubTest()
        //{
        //    string channel = "product:urls:queue";
        //    ISubscriber subscriber = RedisClient.ProdcutUrlsInstance.CurrentClient.GetSubscriber();
        //    Console.WriteLine($"Sub channel of {channel}.");
        //    subscriber.SubscribeAsync(channel, (ch, msg) =>
        //    {
        //        Console.WriteLine($"{ch}: {msg}");
        //    });
        //    Console.WriteLine("The end");
        //    Console.ReadLine();
        //}

        static void Output(string text, ConsoleColor color = ConsoleColor.Black)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
