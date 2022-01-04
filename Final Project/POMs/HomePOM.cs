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
        public string url = "https://www.edgewordstraining.co.uk/demo-site/";
        public By myAccountLink = By.LinkText("My account");
    }
}
