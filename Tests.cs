using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace Data_Drivenn_Test_With_Selenium
{
    public class Tests
    {
        RemoteWebDriver driver;
        IWebElement firstNumfield;
        IWebElement secondNumfield;
        IWebElement operation;
        IWebElement calculateButton;
        IWebElement resetButton;
        IWebElement resultField;

       [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://number-calculator.nakov.repl.co/");
            firstNumfield = driver.FindElementById("number1");
            secondNumfield = driver.FindElementById("number2");
            operation = driver.FindElementById("operation");
            calculateButton = driver.FindElementById("calcButton");
            resetButton = driver.FindElementById("resetButton");
            resultField = driver.FindElementById("result");
        }

        //Test app with valid integer data
        [TestCase("5", "+", "6", "Result: 11")]
        [TestCase("11", "-", "4", "Result: 7")]
        [TestCase("2", "*", "6", "Result: 12")]
        [TestCase("12", "/", "6", "Result: 2")]
       

        // Test app with valid decimal data
        [TestCase("5.55", "+", "6.64", "Result: 12.19")]
        [TestCase("3.21", "-", "2.22", "Result: 0.99")]
        [TestCase("3.14", "*", "6.22", "Result: 19.5308")]
        [TestCase("3.14", "/", "6.22", "Result: 0.504823151125")]

        //Test with negative result
        [TestCase("-6", "+", "-1", "Result: -7")]
        [TestCase("-3.21", "-", "-2.22", "Result: -0.99")]
        [TestCase("-2", "*", "4", "Result: -8")]
        [TestCase("12", "/", "-6", "Result: -2")]
        [TestCase("0", "/", "6", "Result: 0")]

        //Test with Infinity
        [TestCase("Infinity", "+", "-2", "Result: Infinity")]
        [TestCase("Infinity", "-", "4", "Result: Infinity")]
        [TestCase("Infinity", "*", "-6", "Result: -Infinity")]
        [TestCase("Infinity", "/", "6", "Result: Infinity")]
        [TestCase("Infinity", "+", "Infinity", "Result: Infinity")]
        [TestCase("Infinity", "/", "0", "Result: Infinity")]
        [TestCase("12", "/", "0", "Result: Infinity")]

        //Test with invalid result
        [TestCase("Infinity", "/", "-Infinity", "Result: invalid calculation")]
        [TestCase("Infinity", "-", "Infinity", "Result: invalid calculation")]
       
        

        //Test with invalid operation
        [TestCase("-6", "", "-1", "Result: invalid operation")]
        [TestCase("-3.21", " ", "-2.22", "Result: invalid operation")]
        [TestCase("-2", "23432", "4", "Result: invalid operation")]
        [TestCase("12", "fds", "-6", "Result: invalid operation")]

        //Test with invalid input 
        [TestCase("", "", "-1", "Result: invalid input")]
        [TestCase("  ", " ", "-2.22", "Result: invalid input")]
        [TestCase("fasfasfa", "**", "4", "Result: invalid input")]
        [TestCase("!$!@#@", "/", "-6", "Result: invalid input")]


        public void TestForCalculateApp(string num1, string op, string num2, string res)
        {
            //Arange
            resetButton.Click();
            firstNumfield.SendKeys(num1);
            operation.SendKeys(op);
            secondNumfield.SendKeys(num2);

            //Act
            calculateButton.Click();

            //Assert
            Assert.AreEqual(res, resultField.Text);
        }


        [OneTimeTearDown]
        public void Teardown()
        {
            driver.Quit();
        }

    }
}