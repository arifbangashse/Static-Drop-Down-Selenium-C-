using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumLearning
{
    public class WindowHandlers
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";

        }

        [Test]
        public void WindowHandler()
        {
            String email = "mentor@rahulshettyacademy.com";

            String parentWindowId = driver.CurrentWindowHandle;

            driver.FindElement(By.ClassName("blinkingText")).Click();

            String childWindowName = driver.WindowHandles[1];

            driver.SwitchTo().Window(childWindowName);

            String text = driver.FindElement(By.CssSelector(".red")).Text;
            String[] splittedText = text.Split("at");
            String [] trimmedString = splittedText[1].Trim().Split(" ");

            Assert.That(trimmedString[0], Is.EqualTo(email));

            driver.SwitchTo().Window(parentWindowId);

            driver.FindElement(By.Id("username")).SendKeys(trimmedString[0]);
            driver.FindElement(By.Id("password")).SendKeys("learningg");
            driver.FindElement(By.XPath("//input[@value='Sign In']")).Click();
        }
    }
}

