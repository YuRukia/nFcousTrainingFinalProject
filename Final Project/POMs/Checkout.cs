using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Final_Project.POMs
{
    public class CheckoutPOM
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public CheckoutPOM(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }

        public string url = "https://www.edgewordstraining.co.uk/demo-site/checkout/";

        public IWebElement firstName;
        public IWebElement lastName;
        public IWebElement billingAddress;
        public IWebElement billingCity;
        public IWebElement billingPostcode;
        public IWebElement billingPhone;
        public IWebElement billingEmail;

        public void FillFields()
        {
            wait.Until(drv => drv.FindElement(By.CssSelector("input#billing_first_name")).Displayed);
            firstName = driver.FindElement(By.CssSelector("input#billing_first_name"));
            lastName = driver.FindElement(By.CssSelector("input#billing_last_name"));
            billingAddress = driver.FindElement(By.CssSelector("input[name='billing_address_1']"));
            billingCity = driver.FindElement(By.CssSelector("input#billing_city"));
            billingPostcode = driver.FindElement(By.CssSelector("input#billing_postcode"));
            billingPhone = driver.FindElement(By.CssSelector("input#billing_phone"));
            billingEmail = driver.FindElement(By.CssSelector("input#billing_email"));

            firstName.Clear();
            firstName.SendKeys("fName");
            lastName.Clear();
            lastName.SendKeys("lName");
            billingAddress.Clear();
            billingAddress.SendKeys("Example Street");
            billingCity.Clear();
            billingCity.SendKeys("Example City");
            billingPostcode.Clear();
            billingPostcode.SendKeys("TN240US");
            billingPhone.Clear();
            billingPhone.SendKeys("44 113 496 0000");
            billingEmail.Clear();
            billingEmail.SendKeys("example@email.co.uk");
        }

        public void OrderRecievedLoop()
        {
            while (!driver.Url.Contains("order-received"))
            {
                wait.Until(drv =>
                {
                    if (driver.Url.Contains("order-received")) { return true; }

                    try
                    {
                        driver.FindElement(By.XPath("/html//button[@id='place_order']")).Click();
                        return true;
                    }
                    catch (StaleElementReferenceException) { return false; }
                    catch (NoSuchElementException) { return false; }
                    catch (ElementClickInterceptedException) { return false; }
                });
            }
        }
    }
}
