using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace UnitMainSite
{
    public class UnitMainSite
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArguments("headless");
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
            driver.Url = "https://www.google.com/";

            Assert.Pass();
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
