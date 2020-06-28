using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

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

            driver.Url = "https://systell.pl/aktualnosci/";
        }

        [TearDown]
        public void Teardown()
        {
            driver.Close();
        }

        [Test]
        public void UnitNewsSite_PhoneNumberOnSite_True()
        {
            string footer_html = "";
            string footer_path = "/html/body/div[2]/div/footer/div/div[1]/div";
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

            Assert.IsTrue(footer_html.Contains("61 669 04 10") || footer_html.Contains("616690410") || footer_html.Contains("616 690 410"));
        }

        [Test]
        public void UnitNewsSite_AnyPostsOnSite_True()
        {
            IWebElement any_post;

            any_post = driver.FindElement(By.XPath("//*[contains(@id, 'post')]")).FindElement(By.XPath("//*[contains(@class, 'post type-post status-publish format-standard has-post-thumbnail hentry category-aktualnosci')]"));
            Console.WriteLine(any_post.GetAttribute("innerHTML"));

            // Exception will automatically fail the test so no need to check anything
            Assert.Pass();
        }

        [Test]
        public void UnitNewsSite_CorrectTitle_True()
        {
            string news_site_head_path = "/html/head";
            string news_site_head_html;

            IWebElement news_site_body = driver.FindElement(By.XPath(news_site_head_path));
            news_site_head_html = news_site_body.GetAttribute("innerHTML");
            Console.WriteLine(news_site_head_html);

            Assert.IsTrue(news_site_head_html.Contains("<title>Aktualności</title>"));
        }
    }
}
