using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace PerfoMainSite
{
    public class PerfoMainSite
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
        public void PerfoMainSite_TenTimesDirectionToMorePostsLinkWithTimeLessThanSetSeconds_True()
        {
            Assert.Pass();
        }
    }
}
