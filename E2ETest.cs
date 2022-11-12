using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumLearning
{
    public class E2ETest
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
        public void EndToEndFlow()
        {
            String[] expectedProducts = { "iphone X", "Blackberry" };
            IWebElement userName, password, signInButton, terms, checkoutButton;

            userName = driver.FindElement(By.Id("username"));
            password = driver.FindElement(By.Id("password"));
            terms = driver.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input"));
            signInButton = driver.FindElement(By.XPath("//input[@value='Sign In']"));


            userName.SendKeys("rahulshettyacademy");
            password.SendKeys("learning");
            terms.Click();
            signInButton.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));

            IList<IWebElement> products  = driver.FindElements(By.TagName("app-card"));

            foreach(IWebElement product in products)
            {
                if(expectedProducts.Contains(product.FindElement(By.CssSelector(".card-title a")).Text))
                {
                    TestContext.Progress.WriteLine(product.FindElement(By.CssSelector(".card-title a")).Text);
                    product.FindElement(By.CssSelector(".card-footer button")).Click();
                }
                
            }

            checkoutButton = driver.FindElement(By.PartialLinkText("Checkout"));
            checkoutButton.Click();
        }
    }
}

