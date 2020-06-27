using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using OpenQA.Selenium.Interactions;

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

            driver.Url = "https://www.systell.pl/";
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
            string language_changer_id = @"menu-item-wpml-ls-751-pl";
            string english_language_id = @"menu-item-wpml-ls-751-en";
            Actions action = new Actions(driver);

            // hover over language change
            IWebElement language_changer = driver.FindElement(By.Id(language_changer_id));
            action.MoveToElement(language_changer).Perform();

            // press on English
            IWebElement english_language = driver.FindElement(By.Id(english_language_id));
            action.MoveToElement(english_language).Click().Build().Perform();

            // get full body html
            string english_site_body_path = "/html/body";
            string english_site_body_html = "";

            IWebElement english_site_body = driver.FindElement(By.XPath(english_site_body_path));
            english_site_body_html = english_site_body.GetAttribute("innerHTML");
            Console.WriteLine(english_site_body_html);

            Assert.IsTrue(english_site_body_html.Contains(
                    "agree to be contacted via telephone or email in order to be provided with commercial offers of Systell"
                ));
        }

        [Test]
        public void UnitMainSite_PrivacyLinkRedirection_PathCorrect()
        {
            string privacy_text = @"Polityka prywatności";

            IWebElement privacy = driver.FindElement(By.LinkText(privacy_text));
            Console.WriteLine(privacy.GetAttribute("href"));

            Assert.IsTrue(privacy.GetAttribute("href").ToLower().Contains("polityka-prywatnosci"));
        }
    }
}
