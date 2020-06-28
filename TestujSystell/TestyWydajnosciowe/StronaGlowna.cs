using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;
using OpenQA.Selenium.Interactions;

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

            driver.Url = "https://www.systell.pl/";
        }

        [TearDown]
        public void Teardown()
        {
            driver.Close();
        }

        [Test]
        public void PerfoMainSite_TenTimesDirectionToMorePostsLinkWithTimeLessThanSetSeconds_True()
        {
            const int threads = 1;
            bool[] passed = new bool[threads];
            int max_seconds = 3;

            foreach (int thread_num in Enumerable.Range(0, threads))
            {
                int result = TestOnce(thread_num);
                if (result < max_seconds)
                    passed[thread_num] = true;
                else
                    passed[thread_num] = false;
            }

            bool all_true = true;
            Console.WriteLine("");
            foreach (bool x in passed)
            {
                Console.WriteLine(x);
                if (x == false)
                {
                    all_true = false;
                    break;
                }
            }

            Assert.IsTrue(all_true);
        }

        int TestOnce(int thread_num)
        {
            Console.WriteLine(thread_num);
            string more_posts_path = @"//*[@id=""aktualnosci_biale""]/div/div/div/div/div[4]/div/div/a";

            IWebDriver new_driver;
            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArguments("headless");
            new_driver = new ChromeDriver(chromeOptions);
            new_driver.Url = driver.Url;

            IWebElement more_posts = new_driver.FindElement(By.XPath(more_posts_path));
            Actions action = new Actions(new_driver);
            action.MoveToElement(more_posts).Click().Build().Perform();
            Console.WriteLine(new_driver.Url);

            new_driver.Close();
            return 1;

            // TODO: count and return elapsed time, thread everything
        }
    }
}
