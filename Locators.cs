using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumLearning
{
    public class Locators
    {
        IWebDriver driver;

        [SetUp]

        public void StartBrowser()
        {
            //new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise";
        }

        [Test]
        public void LocatorsIdentification()
        {
            IWebElement userName, password, signInButton, errorMessage, blinkingText, terms;

            userName = driver.FindElement(By.Id("username"));
            password = driver.FindElement(By.Id("password"));
            signInButton = driver.FindElement(By.XPath("//input[@value='Sign In']"));
            blinkingText = driver.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));
            terms = driver.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input"));
            

            userName.SendKeys("rahulshettyacademy");
            password.SendKeys("learningg");
            terms.Click();
            signInButton.Click();

            //Thread.Sleep(3000);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementValue(signInButton, "Sign In"));
            errorMessage = driver.FindElement(By.ClassName("alert-danger"));
            TestContext.Progress.WriteLine(errorMessage.Text);
            TestContext.Progress.WriteLine(blinkingText.Text);
            String hrefAttr = blinkingText.GetAttribute("href");

            String expectedValue = "https://rahulshettyacademy.com/documents-request";

            Assert.That(hrefAttr, Is.EqualTo(expectedValue));
        }
    }
}

