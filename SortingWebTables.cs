using System;
using System.Collections;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumLearning
{
    public class SortingWebTables
    {
        IWebDriver driver;

        [SetUp]

        public void StartBrowser()
        {
            driver = new ChromeDriver();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/seleniumPractise/#/offers";
        }

        [Test]

        public void SortingTables()
        {
            ArrayList a = new ArrayList();
            ArrayList b = new ArrayList();
            SelectElement dropDown = new SelectElement(driver.FindElement(By.Id("page-menu")));
            dropDown.SelectByValue("20");

            // Store in Arraylist A

            ArrayList arrayListA = new ArrayList();

            IList<IWebElement> productsA = driver.FindElements(By.XPath("//tr/td[1]"));

            foreach(IWebElement product in productsA)
            {
                a.Add(product.Text);
            }

            a.Sort();

            // Click column

            driver.FindElement(By.XPath("//thead/tr/th[1]")).Click();

            // Grab sorted list in Fresh Arraylist B
            IList<IWebElement> productsB = driver.FindElements(By.XPath("//tr/td[1]"));
            foreach (IWebElement product in productsB)
            {
                b.Add(product.Text);
            }

            // Arralist A to B = equal
            CollectionAssert.AreEqual(a, b);
        }

        [TearDown]
        public void CloseBrowser()
        {
            //driver.Quit();
        }
    }
}

