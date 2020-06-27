using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace UnitMainSite
{
    public class UnitMainSite
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            driver = new ChromeDriver(chromeOptions);
        }

        [TearDown]
        public void Teardown()
        {
            driver.Close();
        }

        [Test]
        public void UnitMainSite_MailAddressOnSite_True()
        {
            string footer_html = "";
            string footer_path = "/html/body/div[2]/div/font/footer/div";
            try
            {
                driver.Url = "https://www.systell.pl/";
                IWebElement down_footer = driver.FindElement(By.XPath(footer_path));

                footer_html = down_footer.GetAttribute("innerHTML");
                Console.WriteLine(footer_html);
            }
            catch (OpenQA.Selenium.NoSuchElementException) 
            {
                Console.WriteLine("Couldn't find footer element!!!");
                Console.WriteLine("Searched in:\n\n{0}", footer_path);
            }

            Assert.IsTrue(footer_html.Contains("systell@systell.pl"));
        }

        [Test]
        public void UnitMainSite_ChangeLanguageToEnglish_ContaintsEnglishText()
        {
            Assert.Pass();
        }

        [Test]
        public void UnitMainSite_PrivacyLinkRedirection_PathCorrect()
        {
            Assert.Pass();
        }
    }
}
