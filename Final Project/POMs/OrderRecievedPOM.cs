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
        public string url = "https://www.edgewordstraining.co.uk/demo-site/checkout/order-received";
        public By orderNumberElement = By.CssSelector(".order > strong");
        public By myAccount = By.CssSelector(".menu-item.menu-item-46.menu-item-object-page.menu-item-type-post_type > a");
    }
}
