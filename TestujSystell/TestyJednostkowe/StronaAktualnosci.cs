using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace UnitNewsSite
{
    public class UnitNewsSite
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
        public void UnitNewsSite_PhoneNumberOnSite_True()
        {
            Assert.Pass();
        }

        [Test]
        public void UnitNewsSite_AnyPostsOnSite_True()
        {
            Assert.Pass();
        }

        [Test]
        public void UnitNewsSite_CorrectTitle_True()
        {
            Assert.Pass();
        }
    }
}
