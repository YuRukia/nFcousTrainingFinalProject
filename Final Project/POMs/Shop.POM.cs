using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Final_Project.POMs
{
    public class ShopPOM
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public ShopPOM(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }

        public string url = "https://www.edgewordstraining.co.uk/demo-site/shop/";
        public IWebElement bottomBanner;
        public IWebElement firstShopItem;
        public IWebElement navBarCart;

        public void CloseBottomBanner()
        {
            wait.Until(drv => drv.FindElement(By.XPath("/html//a[@href='#']")).Displayed);
            bottomBanner = driver.FindElement(By.XPath("/html//a[@href='#']"));

            bottomBanner.Click();
        }

        public void SelectFirstShoppingItem()
        {
            wait.Until(drv => drv.FindElement(By.CssSelector(".columns-3.products :first-child> a:nth-child(2)")).Displayed);
            firstShopItem = driver.FindElement(By.CssSelector(".columns-3.products :first-child> a:nth-child(2)"));
            
            firstShopItem.Click();
        }

        public void NavToCart()
        {
            wait.Until(drv => drv.FindElement(By.CssSelector("ul#site-header-cart .count")).Displayed);
            navBarCart = driver.FindElement(By.CssSelector("ul#site-header-cart .count"));

            navBarCart.Click();
        }
    }
}
