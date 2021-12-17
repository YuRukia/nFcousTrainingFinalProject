using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Final_Project.POMs
{
    public class OrderRecievedPOM
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public OrderRecievedPOM(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }

        public string url = "https://www.edgewordstraining.co.uk/demo-site/order-received/";

        public IWebElement orderNumberElement;
        public string orderNumber;
        public IWebElement myAccount;

        public string RetrieveOrderNumber()
        {
            wait.Until(drv => drv.FindElement(By.CssSelector(".order > strong")).Displayed);
            orderNumberElement = driver.FindElement(By.CssSelector(".order > strong"));
            myAccount = driver.FindElement(By.CssSelector(".menu-item.menu-item-46.menu-item-object-page.menu-item-type-post_type > a"));

            orderNumber = orderNumberElement.GetAttribute("textContent");
            myAccount.Click();

            return orderNumber;
        }
    }
}
