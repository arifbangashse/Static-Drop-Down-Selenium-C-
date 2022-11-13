using System;
using System.Collections;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumLearning
{
    public class AlertActionsAutoSuggestive
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";
        }

        [Test]
        public void Test_Alert()
        {
            String name = "Arif";
            driver.FindElement(By.XPath("//fieldset/input[@id='name']")).SendKeys("Arif");
            driver.FindElement(By.CssSelector("#confirmbtn")).Click();
            String alertText = driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Accept();

            StringAssert.Contains(name, alertText);

        }

        [Test]
        public void Test_AutoSuggestiveDropDown()
        {
            driver.FindElement(By.Id("autocomplete")).SendKeys("Pa");
            Thread.Sleep(3000);

            //ArrayList countryList = new ArrayList();

            IList<IWebElement> countryList = driver.FindElements(By.CssSelector(".ui-menu-item div"));

            foreach(IWebElement country in countryList)
            {
                if (country.Text.Equals("Pakistan"))
                {
                    country.Click();
                }
            }

            TestContext.Progress.WriteLine(driver.FindElement(By.Id("autocomplete")).GetAttribute("value"));

        }

        [Test]
        public void Test_Actions()
        {
            driver.Url = "https://rahulshettyacademy.com/";
            Actions a = new Actions(driver);
            a.MoveToElement(driver.FindElement(By.CssSelector("a.dropdown-toggle"))).Perform();
        }

    }
}

