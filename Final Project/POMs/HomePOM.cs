using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Final_Project.POMs
{
    public class HomePOM
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public HomePOM(IWebDriver driver, WebDriverWait wait) 
        {
            this.driver = driver;
            this.wait = wait;
        }

        public string url = "https://www.edgewordstraining.co.uk/demo-site/";
        public IWebElement myAccountLink;

        public void LoadMyAccount()
        {
            wait.Until(drv => driver.FindElement(By.LinkText("My account")).Displayed);
            myAccountLink = driver.FindElement(By.LinkText("My account"));

            myAccountLink.Click();
        }
    }
}
