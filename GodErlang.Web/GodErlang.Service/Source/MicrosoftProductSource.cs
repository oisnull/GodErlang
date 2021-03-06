﻿using GodErlang.Entity;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace GodErlang.Service.Source
{

    public class MicrosoftProductSource : IProductSource
    {
        private IWebDriver _driver;
        public MicrosoftProductSource(IWebDriver driver)
        {
            this._driver = driver;
        }

        public string GetActualPriceDesc()
        {
            //return _driver.FindElement(By.Id("productPrice"))
            //                .FindElements(By.TagName("meta"))[0]
            //                .GetAttribute("content");
            ReadOnlyCollection<IWebElement> elements = _driver.FindElements(By.XPath("//div[@id='rootContainer_BuyBox']/div/section/div/div/div/p/span"));
            if (elements.Count > 0)
            {
                return elements[0].Text;
            }
            return null;
        }

        public string GetOfferType()
        {
            ReadOnlyCollection<IWebElement> elements = _driver.FindElements(By.XPath("//div[@id='skuSelector']/div/div"));
            if (elements.Count > 0)
            {
                return elements[0].Text;
            }
            return null;
        }

        public string GetOriginId()
        {
            //By by = By.Name("ms.prod_id");
            //IWebElement element = _driver.FindElement(by);
            //return element.GetAttribute("content");
            return new Uri(_driver.Url).Segments.LastOrDefault();
        }

        public string GetPriceCurrency()
        {
            return GetActualPriceDesc();
        }

        public string GetPromotionPriceDesc()
        {
            //WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            //string text = wait.Until(d => d.FindElement(By.XPath(xpath))).Text;
            return null;
        }

        public ProductSourceType GetSourceType()
        {
            return ProductSourceType.Microsoft;
        }

        public string GetTitle()
        {
            return _driver.Title;
        }

        public string ProductImages()
        {
            //return _driver.FindElement(By.Id("productImage")).FindElement(By.TagName("img")).GetAttribute("src");
            ReadOnlyCollection<IWebElement> elements = _driver.FindElements(By.XPath("//div[@id='rootContainer_BuyBox']/div/section/div/div/div/picture/img"));
            if (elements.Count > 0)
            {
                return elements[0].GetAttribute("src");
            }
            return null;
        }
    }
}
