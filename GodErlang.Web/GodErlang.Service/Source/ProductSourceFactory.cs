using GodErlang.Entity;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace GodErlang.Service.Source
{
    public class ProductSourceFactory
    {
        public static IProductSource Builder(IWebDriver driver, ProductSourceType sourceType)
        {
            IProductSource productSource = null;
            switch (sourceType)
            {
                case ProductSourceType.Microsoft:
                    productSource = new MicrosoftProductSource(driver);
                    break;
                case ProductSourceType.Amazon:
                    productSource = new AmazonProductSource(driver);
                    break;
                case ProductSourceType.EBay:
                    productSource = new EBayProductSource(driver);
                    break;
                default:
                    break;
            }

            return productSource;
        }
    }
}
