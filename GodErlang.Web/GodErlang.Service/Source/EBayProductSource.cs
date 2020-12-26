using GodErlang.Entity;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace GodErlang.Service.Source
{

    public class EBayProductSource : IProductSource
    {
        private IWebDriver _driver;
        public EBayProductSource(IWebDriver driver)
        {
            this._driver = driver;
        }

        public string GetActualPriceDesc()
        {
            By by = By.Id("prcIsum");
            IWebElement element = _driver.FindElement(by);
            return element.Text;
        }

        public string GetOfferType()
        {
            IWebElement element = _driver.FindElement(By.Id("shSummary"));
            return element.Text;
        }

        public string GetOriginId()
        {
            int first = _driver.Url.LastIndexOf('/');
            int second = _driver.Url.LastIndexOf('?');
            if (first > 0 && second > 0)
            {
                return _driver.Url.Substring(first + 1, second - first - 1)?.Trim();
            }
            return null;
        }

        public string GetPriceCurrency()
        {
            return null;
        }

        public string GetPromotionPriceDesc()
        {
            return null;
        }

        public ProductSourceType GetSourceType()
        {
            return ProductSourceType.EBay;
        }

        public string GetTitle()
        {
            return _driver.Title;
        }

        public string ProductImages()
        {
            IWebElement element = _driver.FindElement(By.Id("icImg"));
            return element.GetAttribute("src");
        }
    }
}
