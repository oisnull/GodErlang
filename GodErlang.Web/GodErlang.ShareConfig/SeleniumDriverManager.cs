using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.IO;

namespace GodErlang.ShareConfig
{
    public class SeleniumDriverManager
    {
        public static IWebDriver GetFireFoxDriver(string driverDirectory)
        {
            if (string.IsNullOrEmpty(driverDirectory))
                throw new ArgumentNullException("driverDirectory");

            FirefoxOptions firefoxOptions = new FirefoxOptions()
            {
                AcceptInsecureCertificates = true
            };

            string driverWorkDir = Path.Combine(driverDirectory, "SeleniumDriver");
            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(driverWorkDir, "geckodriver.exe");
            //service.FirefoxBinaryPath = @"D:\Program Files (x86)\Mozilla Firefox\firefox.exe";
            return new FirefoxDriver(service, firefoxOptions);
        }

        //https://chromedriver.chromium.org/downloads
        public static IWebDriver GetChromeDriver(string driverDirectory, bool showBrowser = true, bool showCommand = true)
        {
            if (string.IsNullOrEmpty(driverDirectory))
                throw new ArgumentNullException("driverDirectory");

            string driverWorkDir = Path.Combine(driverDirectory, "SeleniumDriver");
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(driverWorkDir);
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
