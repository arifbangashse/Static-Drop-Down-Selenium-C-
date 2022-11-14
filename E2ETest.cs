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
            String[] actualProducts = new string[2]; 
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
                    product.FindElement(By.CssSelector(".card-footer button")).Click();
                }
                
            }

            checkoutButton = driver.FindElement(By.PartialLinkText("Checkout"));
            checkoutButton.Click();

            IList<IWebElement> checkoutCards = driver.FindElements(By.CssSelector("h4 a"));

            for(int i = 0; i< checkoutCards.Count; i++)
            {
                actualProducts[i] = checkoutCards[i].Text;
            }

            Assert.AreEqual(expectedProducts, actualProducts);

            driver.FindElement(By.CssSelector(".btn-success")).Click();
            driver.FindElement(By.Id("country")).SendKeys("Pa");
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("Pakistan")));
            driver.FindElement(By.LinkText("Pakistan")).Click();

            driver.FindElement(By.CssSelector("label[for*='checkbox2']")).Click();

            driver.FindElement(By.CssSelector("[value='Purchase']")).Click();

            String actualText = driver.FindElement(By.CssSelector(".alert-success")).Text;
            String expectedText = "Success! Thank you! Your order will be delivered in next few weeks :-).";
           
            StringAssert.Contains(expectedText, actualText);
        }
    }
}

