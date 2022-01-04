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
        public string url = "https://www.edgewordstraining.co.uk/demo-site/checkout/";
        public By firstName = By.CssSelector("input#billing_first_name");
        public By lastName = By.CssSelector("input#billing_last_name");
        public By billingAddress = By.CssSelector("input[name='billing_address_1']");
        public By billingCity = By.CssSelector("input#billing_city");
        public By billingPostcode = By.CssSelector("input#billing_postcode");
        public By billingPhone = By.CssSelector("input#billing_phone");
        public By billingEmail = By.CssSelector("input#billing_email");
        public By placeOrderButton = By.XPath("/html//button[@id='place_order']");
    }
}
