using GodErlang.Entity;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace GodErlang.Service.Source
{
    public class AmazonProductSource : IProductSource
    {
        private IWebDriver _driver;
        public AmazonProductSource(IWebDriver driver)
        {
            this._driver = driver;
        }

        public string GetActualPriceDesc()
        {
            By by = By.Id("priceblock_ourprice");
            IWebElement element = _driver.FindElement(by);
            return element.Text;
        }

        public string GetOfferType()
        {
            By by = By.XPath("//span[@id='ourprice_shippingmessage']/span");
            IWebElement element = _driver.FindElement(by);
            return element.Text;
        }

        public string GetOriginId()
        {
            return HttpUtility.ParseQueryString(new Uri(_driver.Url).Query)["pf_rd_p"];
        }

        public string GetPriceCurrency()
        {
            By by = By.Id("cerberus-data-metrics");
            IWebElement element = _driver.FindElement(by);
            return element.GetAttribute("data-asin-currency-code");
        }

        public string GetPromotionPriceDesc()
        {
            return null;
        }

        public ProductSourceType GetSourceType()
        {
            return ProductSourceType.Amazon;
        }

        public string GetTitle()
        {
            return _driver.Title;
        }

        public string ProductImages()
        {
            return _driver.FindElement(By.Id("imgTagWrapperId"))
                            .FindElement(By.TagName("img"))
                            .GetAttribute("src");
        }
    }
}
