using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;

namespace Final_Project.POMs
{
    public class MyAccountPOM
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public MyAccountPOM(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }

        public string url = "https://www.edgewordstraining.co.uk/demo-site/my-account/";
        
        public IWebElement username;
        public IWebElement password;
        public IWebElement login;
        public IWebElement logout;
        public IWebElement topNavShop;

        public IWebElement ordersNav;

        public void Login(string uName, string pWord)
        {
            wait.Until(drv => drv.FindElement(By.Id("username")).Displayed);
            username = driver.FindElement(By.Id("username"));
            password = driver.FindElement(By.Id("password"));
            login = driver.FindElement(By.CssSelector("button[name='login']"));

            username.Clear();
            username.SendKeys(uName);
            password.Clear();
            password.SendKeys(pWord);
            login.Click();
        }

        public void CheckLoggedIn()
        {
            wait.Until(drv => drv.FindElement(By.LinkText("Logout")).Displayed);
            logout = driver.FindElement(By.LinkText("Logout"));
        }

        public void NavToShop()
        {
            wait.Until(drv => drv.FindElement(By.XPath("//ul[@id='menu-main']//a[@href='https://www.edgewordstraining.co.uk/demo-site/shop/']")).Displayed);
            topNavShop = driver.FindElement(By.XPath("//ul[@id='menu-main']//a[@href='https://www.edgewordstraining.co.uk/demo-site/shop/']"));
            
            topNavShop.Click();
        }

        public bool checkOrder(string orderNumber)
        {
            wait.Until(drv => drv.FindElement(By.CssSelector(".woocommerce-MyAccount-navigation-link.woocommerce-MyAccount-navigation-link--orders > a")).Displayed);
            ordersNav = driver.FindElement(By.CssSelector(".woocommerce-MyAccount-navigation-link.woocommerce-MyAccount-navigation-link--orders > a"));
            
            ordersNav.Click();
            
            wait.Until(drv => drv.FindElement(By.CssSelector(".woocommerce-orders-table__cell.woocommerce-orders-table__cell-order-number")).Displayed);
            List<IWebElement> orders = new List<IWebElement>();
            orders.AddRange(driver.FindElements(By.CssSelector(".woocommerce-orders-table__cell.woocommerce-orders-table__cell-order-number > a")));

            for (int i = 0; i < orders.Count(); i++)
            {
                string a = orders.ElementAt(i).Text;
                if (a.Remove(0,1) == orderNumber)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
