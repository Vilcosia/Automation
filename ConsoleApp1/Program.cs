using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using AventStack.ExtentReports.Reporter;

namespace Automation
{
    class Tests
    {

    
        static void Main(string[] args)
        {
            // Set up ExtentReports
            var extent = new ExtentReports();
            var htmlReporter = new ExtentHtmlReporter(@"\Users\mapho\OneDrive\Pictures\Documents\Automation.html");
            extent.AttachReporter(htmlReporter);

            // Create a test
            var test = extent.CreateTest("Add User Test");

            try
            {
                // Set up WebDriver
                IWebDriver driver = new ChromeDriver();
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                //Maximising the page
                driver.Manage().Window.Maximize();

                // Navigate to the URL
                driver.Navigate().GoToUrl("http://www.way2automation.com/angularjs-protractor/webtables/");

                // Wait for the User List Table to be visible
                wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

                // Verify page title to ensure the correct page has loaded
                if (driver.Title == "Protractor practice website - WebTables")
                {
                    test.Pass("Page has successfully loaded.");
                }
                else
                {
                    test.Fail("Page failed to load.");
                }

                // Click Add user
                driver.FindElement(By.XPath("//button[contains(text(),'Add User')]")).Click();

                // Add user with unique username
                string uniqueUsername = "user_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                driver.FindElement(By.Name("FirstName")).SendKeys("Lethabo");
                driver.FindElement(By.Name("LastName")).SendKeys("Mosefowa");
                driver.FindElement(By.Name("UserName")).SendKeys(uniqueUsername);
                driver.FindElement(By.Name("Password")).SendKeys("kokik");
                driver.FindElement(By.CssSelector("input[type='radio'][value='15']")).Click();
                driver.FindElement(By.XPath("//select[@name='RoleId']")).SendKeys("Admin");
                driver.FindElement(By.Name("Email")).SendKeys("Lee@gmail.com");
                driver.FindElement(By.Name("Mobilephone")).SendKeys("0760149448");
                driver.FindElement(By.XPath("//button[@class='btn btn-success']")).Click();

                // Verify user is added to the list
                if (driver.FindElement(By.XPath($"//td[contains(text(),'{uniqueUsername}')]")).Displayed)
                {
                    test.Pass("User added successfully.");
                }
                else
                {
                    test.Fail("User not added.");
                }

                driver.Quit();
            }
            catch (Exception ex)
            {
                test.Fail(ex.Message);
            }

            // Flush the report
            extent.Flush();
        }
    }
}
