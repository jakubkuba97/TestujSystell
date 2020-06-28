using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;
using OpenQA.Selenium.Interactions;
using System.Threading;

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
        public void PerfoMainSite_TenTimesDirectionToMorePosts_TimesLessThanFiveSec()
        {
            const int thread_number = 10;
            const int max_seconds = 5;
            bool[] passed = new bool[thread_number];
            Thread[] threads = new Thread[thread_number];

            // declare threads
            foreach (int thread_num in Enumerable.Range(0, thread_number))
            {
                threads[thread_num] = new Thread( () =>
                    {
                        int res = TenTimesDirectionToMorePosts_OneIteration(thread_num);

                        if (res <= max_seconds)
                            passed[thread_num] = true;
                        else
                            passed[thread_num] = false;

                        Console.WriteLine("Thread {0} finished with time {1}", thread_num, res);
                    }
                );
            }

            // start threads
            Console.WriteLine("Starting threads...");
            foreach (var thread in threads)
            {
                thread.Start();
            }

            // wait for threads
            foreach (var thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine("Finished threads.");
            Assert.IsTrue(!passed.Contains(false));
        }

        int TenTimesDirectionToMorePosts_OneIteration (int thread_num)
        {
            Console.WriteLine("\t{0}", thread_num);
            string more_posts_path = @"//*[@id=""aktualnosci_biale""]/div/div/div/div/div[4]/div/div/a";

            // declare new driver and go to same url as main
            IWebDriver new_driver;
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            new_driver = new ChromeDriver(chromeOptions);
            new_driver.Url = driver.Url;

            // move to link element
            IWebElement more_posts = new_driver.FindElement(By.XPath(more_posts_path));
            Actions action = new Actions(new_driver);
            action.MoveToElement(more_posts);

            // start timer
            var watch = System.Diagnostics.Stopwatch.StartNew();

            // click element
            action.Click().Build().Perform();
            Console.WriteLine(new_driver.Url);

            // stop and see timer
            watch.Stop();
            var elapsed_time_s = (int)(watch.ElapsedMilliseconds / 1000);

            new_driver.Close();
            Console.WriteLine("s: {0}\tms: {1}", elapsed_time_s, watch.ElapsedMilliseconds);
            return elapsed_time_s;
        }
    }
}
