using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumLearning
{
    public class FunctionalTest
    {
        IWebDriver driver;

        [SetUp]

        public void StartBrowser()
        {
            driver = new ChromeDriver();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise";
        }

        [Test]

        public void DropDown()
        {
            IWebElement loginAsDropDown = driver.FindElement(By.CssSelector("select.form-control"));
            SelectElement s = new SelectElement(loginAsDropDown);
            s.SelectByText("Teacher");
            s.SelectByValue("consult");
            s.SelectByIndex(0);

            IList <IWebElement> rdos =  driver.FindElements(By.CssSelector("input[type='radio']"));

            foreach(IWebElement radioButton in rdos)
            {
                if (radioButton.GetAttribute("value").Equals("user"))
                {
                    radioButton.Click();
                }
            }
        }
    }
}

