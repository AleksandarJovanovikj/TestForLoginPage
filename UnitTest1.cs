using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LoginPageTest
{
    public class Tests
    {
        private IWebDriver _driver;




        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("https://crusader.bransys.com/");
            _driver.Manage().Window.Maximize();

        }

        [Test]
        public void LoginFieldTest()
        //  2.1 Presence of Login Fields: 
        //Verify that the username and password input fields are present on the login page.
        {
            IWebElement usernameField = _driver.FindElement(By.Id("input-204"));
            IWebElement passwordField = _driver.FindElement(By.Id("input-207"));

            Assert.NotNull(usernameField);
            Assert.NotNull(passwordField);
        }
        [Test]
        public void ImputDataTest()
        //    2.2 Input Data Validation: 
        //Check if it's possible to input data values into the username and password fields.

        {
            IWebElement usernameField = _driver.FindElement(By.Id("input-204"));
            usernameField.SendKeys("Testing@mail.com");
            IWebElement passwordField = _driver.FindElement(By.Id("input-207"));
            passwordField.SendKeys("pass123");

            Assert.AreEqual("Testing@mail.com", usernameField.GetAttribute("value"));
            Assert.AreEqual("pass123", passwordField.GetAttribute("value"));
        }

        [Test]
        public void ErrorMessageTest()
        //   2.3 Generic Messages Displayed:
        // Ensure that correct generic error messages are displayed when invalid credentials are entered.*/
        {

            IWebElement usernameField = _driver.FindElement(By.Id("input-204"));
            usernameField.SendKeys("Testing@mail.com");
            IWebElement passwordField = _driver.FindElement(By.Id("input-207"));
            passwordField.SendKeys("pass123");
            IWebElement signInButton = _driver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div/div/div/div/div[2]/div[2]/form/div[2]/div[1]/button"));
            signInButton.Submit();
            Thread.Sleep(1000);
            IWebElement errorMessage = _driver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div/div/div/div/div[2]/div[2]/form/div[1]/div[3]"));
            string expectedErrorMessage = "Incorrect email/username or password";
            string actualerrorMessage = errorMessage.Text;

            Assert.IsNotNull(errorMessage);
            Assert.IsTrue(errorMessage.Text.Contains("Incorrect email/username or password"));
            Assert.AreEqual(expectedErrorMessage, actualerrorMessage);

        }



        [TearDown]
        public void TearDown()
        {
            Thread.Sleep(1000);
            _driver.Close();
        }



    }
}