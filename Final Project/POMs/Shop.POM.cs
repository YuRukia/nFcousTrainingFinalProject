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
        public string url = "https://www.edgewordstraining.co.uk/demo-site/shop/";
        public By bottomBanner = By.XPath("/html//a[@href='#']");
        public By firstShopItem = By.CssSelector(".columns-3.products :first-child> a:nth-child(2)");
        public By navBarCart = By.CssSelector("ul#site-header-cart .count");
    }
}
