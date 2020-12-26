using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Text;

namespace GodErlang.ConsoleTest
{
    public class WebDriverLoader
    {
        const string DRIVER_DIRECTORY = @"E:\ExtInfo\github\GodErlang\GodErlang.Web\GodErlang.ConsoleTest\driver\";

        public static IWebDriver GetFireFoxDriver()
        {
            FirefoxOptions firefoxOptions = new FirefoxOptions()
            {
                AcceptInsecureCertificates = true
            };

            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(DRIVER_DIRECTORY, "geckodriver.exe");
            service.FirefoxBinaryPath = @"D:\Program Files (x86)\Mozilla Firefox\firefox.exe";
            return new FirefoxDriver(service, firefoxOptions);
        }

        public static IWebDriver GetChromeDriver(bool showBrowser = true, bool showCommand = true)
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(DRIVER_DIRECTORY);
            if (showCommand)
                service.HideCommandPromptWindow = true;

            ChromeOptions chromeOptions = new ChromeOptions()
            {
                AcceptInsecureCertificates = true
            };
            if (!showBrowser)
            {
                chromeOptions.AddArgument("--headless");
                chromeOptions.AddArgument("--disable-gpu");
            }
            chromeOptions.AddArgument("--no-sandbox");
            chromeOptions.AddArgument("--ignore-certificate-errors");
            chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
            return new ChromeDriver(service, chromeOptions);
        }
    }
}
